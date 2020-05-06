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

$('.custom-file-input').change(function (e) {
    e.preventDefault();
    var file = e.target.files[0];
    var fileExtension = file.name.replace(/^.*\./, '');
    var output = document.getElementById('previewImg');
    switch (fileExtension) {
        case 'png': case 'jpeg': case 'jpg':
            $(this).next('.custom-file-label').text(file.name);

            output.src = URL.createObjectURL(file);
            output.onload = function () {
                URL.revokeObjectURL(output.src)
            }
            break;
        default:
            $(this).next('.custom-file-label').text("Invalid Source File");
            break;
    }
});