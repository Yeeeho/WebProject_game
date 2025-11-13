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
});