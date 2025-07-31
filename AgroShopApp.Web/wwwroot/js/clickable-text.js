document.addEventListener('DOMContentLoaded', function () {
    const rows = document.querySelectorAll('.clickable-row');
    rows.forEach(row => {
        row.style.cursor = 'pointer';
        row.addEventListener('click', () => {
            const href = row.getAttribute('data-href');
            if (href) window.location.href = href;
        });
    });
});