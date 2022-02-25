/*!
* Start Bootstrap - Simple Sidebar v6.0.3 (https://startbootstrap.com/template/simple-sidebar)
* Copyright 2013-2021 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-simple-sidebar/blob/master/LICENSE)
*/
// 
// Scripts
// 

window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

});
$(function () {
    var PlaceHolderElement = $('#PlaceHolderHere');
    $('button[data-toggle="ajax-modal"]').click(function (event) {
        //console.log("clicked");
        var url = $(this).data('url');
        var decodeUrl = decodeURIComponent(url);
        $.get(decodeUrl).done(function (data) {
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })
})
//$(document).on("click", ".add-banner", function () {
//    var pageId = $(this).data('id');
//    $("#pageId").val(pageId);
//    console.log($("#pageId").val());
//});

//$(document).on("click", ".template-box", function () {
    
//});

//$('.template-box img').click(function () {
//    var pageId = $("#pageId").val();
//    var templateId = $(this).data('id');
//    console.log(pageId);
//    console.log(templateId);
//    $(".modal-body #pageId").val(pageId);
//    $(".modal-body #templateId").val(templateId);

//    $('#addBannerModal').hide();
//    $('#selectTemplateModal').show();

//})

//$('.save-template2').click(function () {
//    $('#selectTemplateModal').hide();
//    $('#sortBanner').show();
//})






