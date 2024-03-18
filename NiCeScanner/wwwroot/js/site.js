// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    var links = document.querySelectorAll('.nav-link');
    var currentPageUrl = window.location.href;

    links.forEach(function (link) {
        var linkUrl = link.getAttribute('href');

        if (currentPageUrl.includes(linkUrl) && linkUrl !== '/') {
            link.parentElement.classList.add('active');
        }
    });
});
