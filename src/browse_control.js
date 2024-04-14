function Nextmusic() {
    document.querySelector('tp-yt-paper-icon-button[title="다음"]').click();
}

function Previousmusic() {
    document.querySelector('tp-yt-paper-icon-button[title="이전"]').click();
}
//need to implement
function setVolume(volume) {
    // 음량 조절 명령
    document.getElementById('player').volume = volume / 100;
}

function toggleVideoPlayback() {
    document.getElementById('play-pause-button').click();
}

function bringVideoTime() {
    var current_time;
    current_time = document.getElementById('progress-bar').value;
    return current_time;
}

//수정
function videoProgressChanged(value) {
    dotnetHelper.invokeMethodAsync('OnVideoProgressChanged', value);
}