export function renderDashboardCharts(chartData) {
    const ordersCtx = document.getElementById('ordersChart')?.getContext('2d');
    const revenueCtx = document.getElementById('revenueChart')?.getContext('2d');
    const bestCtx = document.getElementById('bestSellersChart')?.getContext('2d');

    if (ordersCtx && chartData.orders) {
        new Chart(ordersCtx, {
            type: 'line',
            data: {
                labels: chartData.orders.labels,
                datasets: [{
                    label: 'Orders',
                    data: chartData.orders.data,
                    backgroundColor: 'rgba(40, 167, 69, 0.2)',
                    borderColor: 'rgba(40, 167, 69, 1)',
                    borderWidth: 2,
                    tension: 0.2
                }]
            },
            options: {
                responsive: true,
                scales: { y: { beginAtZero: true } }
            }
        });
    }

    if (revenueCtx && chartData.revenue) {
        new Chart(revenueCtx, {
            type: 'line',
            data: {
                labels: chartData.revenue.labels,
                datasets: [{
                    label: 'Revenue',
                    data: chartData.revenue.data,
                    backgroundColor: 'rgba(255, 193, 7, 0.2)',
                    borderColor: 'rgba(255, 193, 7, 1)',
                    borderWidth: 2,
                    fill: true,
                    tension: 0.3
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    title: { display: true, color: '#fff' },
                    legend: { labels: { color: '#fff' } }
                },
                scales: {
                    x: { ticks: { color: '#ccc' } },
                    y: {
                        ticks: {
                            color: '#ccc',
                            callback: value => '$' + value.toLocaleString()
                        }
                    }
                }
            }
        });
    }

    if (bestCtx && chartData.bestSellers) {
        new Chart(bestCtx, {
            type: 'bar',
            data: {
                labels: chartData.bestSellers.labels,
                datasets: [{
                    label: 'Quantity Sold',
                    data: chartData.bestSellers.data,
                    backgroundColor: 'rgba(13, 202, 240, 0.6)',
                    borderColor: 'rgba(13, 202, 240, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    title: { display: true, color: '#fff' },
                    legend: { display: false }
                },
                scales: {
                    x: { ticks: { color: '#ccc' } },
                    y: {
                        beginAtZero: true,
                        ticks: { color: '#ccc' }
                    }
                }
            }
        });
    }
}