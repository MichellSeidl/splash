function switchPages(id, page) {
    var curr = document.getElementById('current_page').value;

    $('#' + curr).toggleClass('active-page');
    $('#' + id).toggleClass('active-page');

    document.getElementById('current_page').value = id;


    var displayed_page = document.getElementById('displayed_page').value;
    $('#' + displayed_page).toggleClass('d-none');
    $('#' + page).toggleClass('d-none');

    document.getElementById('displayed_page').value = page;
}