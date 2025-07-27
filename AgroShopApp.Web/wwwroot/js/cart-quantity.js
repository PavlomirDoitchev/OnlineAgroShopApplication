document.addEventListener("DOMContentLoaded", () => {
    const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

    document.querySelectorAll(".quantity-input").forEach(input => {
        input.addEventListener("change", () => {
            const productId = input.dataset.productId;
            const quantity = parseInt(input.value, 10);

            if (isNaN(quantity) || quantity < 1) {
                showToast("Invalid quantity entered.", "danger");
                input.value = input.min;
                return;
            }

            updateQuantity(productId, quantity, input, token);
        });
    });
});

async function updateQuantity(productId, quantity, input, token) {
    try {
        const response = await fetch("/Cart/UpdateQuantity", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "RequestVerificationToken": token
            },
            body: JSON.stringify({ productId, quantity })
        });

        if (!response.ok) {
            throw new Error("Failed to update quantity.");
        }

        const data = await response.json();

        // Update input to reflect corrected quantity
        if (input && data.correctedQuantity !== quantity) {
            input.value = data.correctedQuantity;
            showToast(`Quantity capped at available stock (${data.correctedQuantity}).`, "warning");
        } else {
            showToast(`Quantity updated to ${data.correctedQuantity}.`, "success");
        }

        // Update per-item total cell
        const itemTotalCell = document.querySelector(`.item-total[data-product-id="${productId}"]`);
        if (itemTotalCell && data.itemTotal != null) {
            itemTotalCell.textContent = formatCurrency(data.itemTotal);
        }

        // Update grand total
        const grandTotalEl = document.getElementById("grandTotal");
        if (grandTotalEl && data.grandTotal != null) {
            grandTotalEl.textContent = formatCurrency(data.grandTotal);
        }

    } catch (error) {
        showToast(error.message, "danger");
    }
}

function showToast(message, type = "success") {
    const toastContainer = document.getElementById("ajaxToastContainer");

    const toast = document.createElement("div");
    toast.className = `toast align-items-center text-white border-0 bg-${type} show`;
    toast.setAttribute("role", "alert");
    toast.style.minWidth = "250px";

    toast.innerHTML = `
        <div class="d-flex">
            <div class="toast-body fw-semibold">${message}</div>
            <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast" aria-label="Close"></button>
        </div>
    `;

    toastContainer.appendChild(toast);

    const bsToast = new bootstrap.Toast(toast, { delay: 3000 });
    bsToast.show();

    toast.addEventListener("hidden.bs.toast", () => toast.remove());
}

function formatCurrency(value) {
    return new Intl.NumberFormat("en-US", {
        style: "currency",
        currency: "USD"
    }).format(value);
}
