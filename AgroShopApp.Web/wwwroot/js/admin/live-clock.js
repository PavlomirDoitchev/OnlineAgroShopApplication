export function startLiveClock(elementId = 'clock') {
    function updateClock() {
        const now = new Date();
        const options = {
            weekday: 'long',
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit'
        };
        const clockEl = document.getElementById(elementId);
        if (clockEl) {
            clockEl.textContent = now.toLocaleString('en-US', options);
        }
    }

    updateClock();
    setInterval(updateClock, 1000);
}