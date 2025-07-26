$(document).on("click", ".category-filter", function (e) {
    e.preventDefault();

    const catId = $(this).data("category");
    const search = $(this).data("search") || "";

    $.get('/Product/Index', {
        categoryId: catId,
        searchTerm: search
    }, function (result) {
        $("#productsContainer").html(result);
        $("input[name='categoryId']").val(catId);
    });
});

// AJAX Search Form
$("#searchForm").on("submit", function (e) {
    e.preventDefault();
    const url = '/Product/Index';
    const data = $(this).serialize();

    $.get(url, data, function (result) {
        $("#productsContainer").html(result);
    });
});
// Category Button Filter
$(document).on("click", ".category-filter", function (e) {
    e.preventDefault();
    const catId = $(this).data("category") ?? "";
    const search = $("input[name='searchTerm']").val();

    $.get('/Product/Index', {
        categoryId: catId,
        searchTerm: search
    }, function (result) {
        $("#productsContainer").html(result);
        $("input[name='categoryId']").val(catId);
    });
});