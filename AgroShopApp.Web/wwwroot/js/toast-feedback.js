window.addEventListener('DOMContentLoaded', () => {
    const toastEl = document.getElementById('feedbackToast');
    if (toastEl) {
        const toast = new bootstrap.Toast(toastEl, { delay: 3000 });
        toast.show();
    }
});