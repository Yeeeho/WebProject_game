mergeInto(LibraryManager.library, {

    Status: function(isGameOver, score) {
        window.isGameOver = isGameOver;
        window.gameScore = score;
        console.log("유니티로부터 값을 받음");
        console.log("isGameOver:" + isGameOver);
        console.log("score:" + score);

        const eventDetail = {
            isGameOver: isGameOver,
            score: score
        };

        const unityEvent = new CustomEvent("unityGameStatus", {
            detail: eventDetail
        });

        document.dispatchEvent(unityEvent);
    },

    // 1. 마이크 초기화 및 권한 요청
    InitMicrophoneJS: function () {
    // 이미 초기화되었으면 패스
    if (window.audioContext) return;

    // 브라우저 호환성 체크
    window.AudioContext = window.AudioContext || window.webkitAudioContext;
    if (!window.AudioContext) {
        console.error("Web Audio API is not supported in this browser");
        return;
    }

    navigator.mediaDevices.getUserMedia({ audio: true })
      .then(function (stream) {
        // 오디오 컨텍스트 생성
        window.audioContext = new AudioContext();
        window.analyser = window.audioContext.createAnalyser();
        window.microphone = window.audioContext.createMediaStreamSource(stream);
        window.javascriptNode = window.audioContext.createScriptProcessor(2048, 1, 1);

        // 분석기 설정
        window.analyser.smoothingTimeConstant = 0.8;
        window.analyser.fftSize = 1024;

        // 연결: 마이크 -> 분석기 -> (스피커로는 안 나가게 처리)
        window.microphone.connect(window.analyser);
        window.analyser.connect(window.javascriptNode);
        window.javascriptNode.connect(window.audioContext.destination);

        console.log("마이크 연결 성공!");
      })
      .catch(function (err) {
        console.error("마이크 권한 거부됨: " + err);
      });
    },

    // 2. 현재 볼륨 가져오기 (유니티가 매 프레임 호출할 함수)
    GetMicrophoneVolumeJS: function () {
        if (!window.analyser) return 0.0;

        var array = new Uint8Array(window.analyser.frequencyBinCount);
        window.analyser.getByteFrequencyData(array);

        var values = 0;
        var length = array.length;

        // 평균 볼륨 계산
        for (var i = 0; i < length; i++) {
        values += array[i];
        }

        // 0~1 사이의 값으로 정규화해서 리턴
        return values / length / 255.0;
    }
});