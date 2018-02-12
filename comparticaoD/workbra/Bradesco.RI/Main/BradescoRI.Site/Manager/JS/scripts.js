function downsel(num, numcheck) {
    if (document.getElementById(numcheck).checked == "0") {
        document.getElementById(num).style.backgroundColor = "#FFFFFF";
        //document.getElementById(numcheck).checked = 1;
    }
    else {
        document.getElementById(num).style.backgroundColor = "#F7F8F9";
        //document.getElementById(numcheck).checked = 0;
    }
}
function playvid() {
    var video = document.getElementById('video0');
    if (video.paused === false) {
        video.pause();
        document.getElementById('vid-alta-play').style.display = "block";
    } else {
        video.play();
        document.getElementById('vid-alta-play').style.display = "none";
    }

    return false;
}