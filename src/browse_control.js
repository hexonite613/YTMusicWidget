function playMusic() {
    // 음악 재생 명령
    document.getElementById('player').play();
}

function pauseMusic() {
    // 음악 일시 정지 명령
    document.getElementById('player').pause();
}

function setVolume(volume) {
    // 음량 조절 명령
    document.getElementById('player').volume = volume / 100;
}

function toggleVideoPlayback() {
    var videoElement = document.getElementById('player');
    if (videoElement) {
        if (videoElement.paused) {
            videoElement.play();
        } else {
            videoElement.pause(); 
        }
        return !videoElement.paused; 
    } else {
        return false; 
    }
}