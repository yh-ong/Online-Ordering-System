// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$(window).scroll(function () {
    $('.slideanim').each(function () {
        var pos = $(this).offset().top;
        var top = $(window).scrollTop();
        if (pos < top + 600) {
            $(this).addClass("slide");
        }
    });

    var top =  $(window).scrollTop();
    if (top > 50) {
        $('#search-navbar').css('top', '-100px');
        if (top > 100) {
            $('#search-navbar').addClass("search-navbar-top").removeAttr('style');
        }
    } else {
        $('#search-navbar').removeClass("search-navbar-top").removeAttr('style');
    }

});



$('.owl-carousel').owlCarousel({
    loop: true,
    margin: 10,
    nav: false,
    autoplay: false,
    autoplayTimeout: 5000,
    autoplayHoverPause: false,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 3
        },
        1000: {
            items: 5
        }
    }
});