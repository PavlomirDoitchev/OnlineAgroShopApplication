document.addEventListener("DOMContentLoaded", () => {
    const carousel = document.getElementById('agroCarousel');
    if (carousel) {
        new bootstrap.Carousel(carousel, {
            interval: 4000,
            ride: 'carousel'
        });
    }
});