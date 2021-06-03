$(document).ready(function() {

    $('.lightshow').lightshow({
        autoplay: false, // (boolean) animate automatically 
        pause: true, // (boolean) pause on hover 
        duration: 5000, // (integer) single slide duration, in milliseconds 
        animation: 400, // (integer) animation duration, in milliseconds
        transition: "slide", // (string)  transition between slides 
        controls: true, // (boolean) show controls 
        big_controls: false, // (boolean) big controls - half of an image
        title: true, // (boolean) show title from 'data-title' attribute of <li>
        change_url: true, // (boolean) put current slide number into url
        keyboard: true, // (boolean) enables keyboard navigation - left and right arrow
        responsive: true
    });

});