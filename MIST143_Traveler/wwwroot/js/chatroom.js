let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    connection.invoke("GetConnectionId").then(function (id) {
        document.getElementById("yourID").innerText = id;
    });
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("AclientMessageFunction", function (input_message, currentUser) {
    //取得內容
    const chat = document.getElementById('chatroom2');
    const input = document.getElementById('AclientInput');
    const Cur_ID = document.getElementById('yourID').innerText;
    //取得時間
    let Today = new Date();
    let time = `${Today.getHours()}點${Today.getMinutes()}分`;

    //創造訊息，判斷是本人還是分本人套用不同CSS
    var div = document.createElement("div");
    div.setAttribute("class", "bubbleWrapper");
    let message = "";
    if (currentUser == Cur_ID) {
        message = `
        <div class="inlineContainer">
            <img class="inlineIcon" src="https://cdn1.iconfinder.com/data/icons/ninja-things-1/1772/ninja-simple-512.png">
            <div class="otherBubble other">
                <p class="inlineP">${input_message}</p>
            </div>
        </div><span class="other">${time}</span>`
    }
    else {
        message = `
            <div class="inlineContainer own">
            <img class="inlineIcon" src="https://www.pinclipart.com/picdir/middle/205-2059398_blinkk-en-mac-app-store-ninja-icon-transparent.png">
            <div class="ownBubble own">
                 <p class="inlineP">${input_message}</p>
            </div>
            </div><span class="own">${time}</span>`
    }
    div.innerHTML = message;
    chat.appendChild(div);
    chat.scrollTop = chat.scrollHeight;
    input.value = "";

});

const inputA = document.getElementById('AclientInput');
inputA.addEventListener("keypress", function (event) {
    if (event.key === "Enter") {
        event.preventDefault();
        document.getElementById("Aclick").click();
    }
});


function Aclient() {
    connection.invoke("AclientMessage", inputA.value);
}

function showChatRoom() {
    $('.chatroomBorder').toggle("normal")
}

function chatDemoBtn1() {
    inputA.value = "您好，你們的樂高樂園行程已經全部賣完了，請問近期還會開團嗎?";   
}
function chatDemoBtn2() {
    inputA.value = "您好，我是您的星球旅遊服務專員Miyo林，很高興為您服務。您可以先將該行程加入我的最愛，如果有其他的客人取消行程，將會寄送候補通知E-Mail給您。";
}
function chatDemoBtn3() {
    inputA.value = "了解，謝謝您～～";
}
function chatDemoBtn4() {
    inputA.value = "不客氣，感謝您使用星球旅遊的服務！";
}