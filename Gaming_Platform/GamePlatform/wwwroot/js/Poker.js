let cardClicked1 = false;
let cardClicked2 = false;
let cardClicked3 = false;
let cardClicked4 = false;
let cardClicked5 = false;
let isCheckBlocked = true;

async function newGame() {
    await dealCards();
    await resetChangeCardsButtons()
    isCheckBlocked = false
    await changeCard1()
    await changeCard2()
    await changeCard3()
    await changeCard4()
    await changeCard5()
    resetChangeCardsButtons()
}

function checkGame() {
    if (!isCheckBlocked) {
        $.ajax({
            type: "POST",
            url: "/Poker/CheckGame",
            success: function (response) {
                showWinner(response)
            },
            error: function (response) {
                console.log("Coś poszło nie tak" + response)
            }
        })
        isCheckBlocked = true
    }

}

function showWinner(response) {
    let respArray = response.split(";")
    document.getElementById("poker-overlay").style.display = "block";
    if (respArray[0] === "draw") {
        document.getElementById("poker-winner").innerHTML = "Draw";
    } else {
       document.getElementById("poker-winner").innerHTML = "The winner is " + respArray[0];
    }
    document.getElementById("poker-layout").innerHTML = "With " + respArray[1] + " cards layout";
}

function closeOverlay() {
    document.getElementById("poker-overlay").style.display = "none";
}

function resetChangeCardsButtons() {
    cardClicked1 = false;
    cardClicked2 = false;
    cardClicked3 = false;
    cardClicked4 = false;
    cardClicked5 = false;
}

function changeCard1() {
    if (!cardClicked1 && !isCheckBlocked) {
        changeCard(1);
        cardClicked1 = true;
    }
}

function changeCard2() {
    if (!cardClicked2 && !isCheckBlocked) {
        changeCard(2);
        cardClicked2 = true;
    }
}

function changeCard3() {
    if (!cardClicked3 && !isCheckBlocked) {
        changeCard(3);
        cardClicked3 = true;
    }
}

function changeCard4() {
    if (!cardClicked4 && !isCheckBlocked) {
        changeCard(4);
        cardClicked4 = true;
    }
}

function changeCard5() {
    if (!cardClicked5 && !isCheckBlocked) {
        changeCard(5);
        cardClicked5 = true;
    }
}

function dealCards() {
    $.ajax({
        type: "POST",
        url: "/Poker/DealCards",
        success: function (response) {
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

function getPlayerHand() {
    $.ajax({
        type: "POST",
        url: "/Poker/GetPlayerHand",
        success: function (response) {
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

function getComputerHand() {
    $.ajax({
        type: "POST",
        url: "/Poker/GetComputerHand",
        success: function (response) {
            displayCards(response)
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

function changeCard(Id) {
    $.ajax({
        type: "POST",
        data: { Id },
        url: "/Poker/changeCard",
        success: function (response) {
            displayCards(response)
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

function displayCards(response) {
    document.getElementById("card-1").style.backgroundImage = setBackgroundCard(response.card1Suit + response.card1Value);
    document.getElementById("card-2").style.backgroundImage = setBackgroundCard(response.card2Suit + response.card2Value);
    document.getElementById("card-3").style.backgroundImage = setBackgroundCard(response.card3Suit + response.card3Value);
    document.getElementById("card-4").style.backgroundImage = setBackgroundCard(response.card4Suit + response.card4Value);
    document.getElementById("card-5").style.backgroundImage = setBackgroundCard(response.card5Suit + response.card5Value);
}

function setBackgroundCard(cardId) {
    switch (cardId) {
        case 'CLUBSTWO':
            return "url('/assets/cards/2C.jpg')";
        case 'DIAMONDSTWO':
            return "url('/assets/cards/2D.jpg')";
        case 'HEARTSTWO':
            return "url('/assets/cards/2H.jpg')";
        case 'SPADESTWO':
            return "url('/assets/cards/2S.jpg')";
        case 'CLUBSTHREE':
            return "url('/assets/cards/3C.jpg')";
        case 'DIAMONDSTHREE':
            return "url('/assets/cards/3D.jpg')";
        case 'HEARTSTHREE':
            return "url('/assets/cards/3H.jpg')";
        case 'SPADESTHREE':
            return "url('/assets/cards/3S.jpg')";
        case 'CLUBSFOUR':
            return "url('/assets/cards/4C.jpg')";
        case 'DIAMONDSFOUR':
            return "url('/assets/cards/4D.jpg')";
        case 'HEARTSFOUR':
            return "url('/assets/cards/4H.jpg')";
        case 'SPADESFOUR':
            return "url('/assets/cards/4S.jpg')";
        case 'CLUBSFIVE':
            return "url('/assets/cards/5C.jpg')";
        case 'DIAMONDSFIVE':
            return "url('/assets/cards/5D.jpg')";
        case 'HEARTSFIVE':
            return "url('/assets/cards/5H.jpg')";
        case 'SPADESFIVE':
            return "url('/assets/cards/5S.jpg')";
        case 'CLUBSSIX':
            return "url('/assets/cards/6C.jpg')";
        case 'DIAMONDSSIX':
            return "url('/assets/cards/6D.jpg')";
        case 'HEARTSSIX':
            return "url('/assets/cards/6H.jpg')";
        case 'SPADESSIX':
            return "url('/assets/cards/6S.jpg')";
        case 'CLUBSSEVEN':
            return "url('/assets/cards/7C.jpg')";
        case 'DIAMONDSSEVEN':
            return "url('/assets/cards/7D.jpg')";
        case 'HEARTSSEVEN':
            return "url('/assets/cards/7H.jpg')";
        case 'SPADESSEVEN':
            return "url('/assets/cards/7S.jpg')";
        case 'CLUBSEIGHT':
            return "url('/assets/cards/8C.jpg')";
        case 'DIAMONDSEIGHT':
            return "url('/assets/cards/8D.jpg')";
        case 'HEARTSEIGHT':
            return "url('/assets/cards/8H.jpg')";
        case 'SPADESEIGHT':
            return "url('/assets/cards/8S.jpg')";
        case 'CLUBSNINE':
            return "url('/assets/cards/9C.jpg')";
        case 'DIAMONDSNINE':
            return "url('/assets/cards/9D.jpg')";
        case 'HEARTSNINE':
            return "url('/assets/cards/9H.jpg')";
        case 'SPADESNINE':
            return "url('/assets/cards/9S.jpg')";
        case 'CLUBSTEN':
            return "url('/assets/cards/10C.jpg')";
        case 'DIAMONDSTEN':
            return "url('/assets/cards/10D.jpg')";
        case 'HEARTSTEN':
            return "url('/assets/cards/10H.jpg')";
        case 'SPADESTEN':
            return "url('/assets/cards/10S.jpg')";
        case 'CLUBSJACK':
            return "url('/assets/cards/JC.jpg')";
        case 'DIAMONDSJACK':
            return "url('/assets/cards/JD.jpg')";
        case 'HEARTSJACK':
            return "url('/assets/cards/JH.jpg')";
        case 'SPADESJACK':
            return "url('/assets/cards/JS.jpg')";
        case 'CLUBSQUEEN':
            return "url('/assets/cards/QC.jpg')";
        case 'DIAMONDSQUEEN':
            return "url('/assets/cards/QD.jpg')";
        case 'HEARTSQUEEN':
            return "url('/assets/cards/QH.jpg')";
        case 'SPADESQUEEN':
            return "url('/assets/cards/QS.jpg')";
        case 'CLUBSKING':
            return "url('/assets/cards/KC.jpg')";
        case 'DIAMONDSKING':
            return "url('/assets/cards/KD.jpg')";
        case 'HEARTSKING':
            return "url('/assets/cards/KH.jpg')";
        case 'SPADESKING':
            return "url('/assets/cards/KS.jpg')";
        case 'CLUBSACE':
            return "url('/assets/cards/AC.jpg')";
        case 'DIAMONDSACE':
            return "url('/assets/cards/AD.jpg')";
        case 'HEARTSACE':
            return "url('/assets/cards/AH.jpg')";
        case 'SPADESACE':
            return "url('/assets/cards/AS.jpg')";
    }
}
