let cardClicked1 = false;
let cardClicked2 = false;
let cardClicked3 = false;
let cardClicked4 = false;
let cardClicked5 = false;
let isCheckBlocked = true;

window.onload = function () {
    getAllAnnouncements()
}

function RegisterUser() {
    document.getElementById("dark-overlay-id").style.visibility = "visible"
    document.getElementById("register-container-id").style.visibility = "visible"
    document.getElementById("login-container-id").style.visibility = "hidden"
    document.getElementById("new-announcement-container-id").style.visibility = "hidden"
}

function cancelRegisterUser() {
    document.getElementById("register-container-id").style.visibility = "hidden"
    document.getElementById("dark-overlay-id").style.visibility = "hidden"
    clearRegisterUserForm()
}

function Login() {
    document.getElementById("dark-overlay-id").style.visibility = "visible"
    document.getElementById("login-container-id").style.visibility = "visible"
    document.getElementById("register-container-id").style.visibility = "hidden"
    document.getElementById("new-announcement-container-id").style.visibility = "hidden"
}

function cancelLoginUser() {
    document.getElementById("login-container-id").style.visibility = "hidden"
    document.getElementById("dark-overlay-id").style.visibility = "hidden"
}

function NewAnnouncement() {
    document.getElementById("dark-overlay-id").style.visibility = "visible"
    document.getElementById("new-announcement-container-id").style.visibility = "visible"
    document.getElementById("register-container-id").style.visibility = "hidden"
    document.getElementById("login-container-id").style.visibility = "hidden"
}

function cancelNewAnnouncement() {
    document.getElementById("new-announcement-container-id").style.visibility = "hidden"
    document.getElementById("dark-overlay-id").style.visibility = "hidden"
    clearNewAnnouncementForm()
}

function clearRegisterUserForm() {
    document.getElementById("fname").value = ""
    document.getElementById("lname").value = ""
    document.getElementById("mail").value = ""
    document.getElementById("login").value = ""
    document.getElementById("pass").value = ""
}

function clearNewAnnouncementForm() {
    document.getElementById("title").value = ""
    document.getElementById("author").value = ""
    document.getElementById("category").value = ""
    document.getElementById("city").value = ""
    document.getElementById("description").value = ""
}

function addUser() {
    fname = document.getElementById("fname").value
    lname = document.getElementById("lname").value
    mail = document.getElementById("mail").value
    login = document.getElementById("login").value
    pass = document.getElementById("pass").value
    $.ajax({
        type: "POST",
        data: { firstName: fname, lastName: lname, email: mail, login: login, password: pass },
        url: "/Poker/AddUser",
        success: function (response) {
            clearRegisterUserForm()
            cancelRegisterUser();
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

function addAnnouncement() {
    let today = new Date()
    let dd = String(today.getDate()).padStart(2, '0')
    let mm = String(today.getMonth() + 1).padStart(2, '0')
    let yyyy = today.getFullYear()

    title = document.getElementById("title").value
    author = document.getElementById("author").value
    category = document.getElementById("category").value
    city = document.getElementById("city").value
    description = document.getElementById("description").value
    date = mm + '/' + dd + '/' + yyyy;

    $.ajax({
        type: "POST",
        data: { date: date, city: city, category: category, title: title, author: author, description: description },
        url: "/Poker/AddAnnouncement",
        success: function (response) {
            clearNewAnnouncementForm()
            cancelNewAnnouncement()
            location.reload()
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

function getAllUsers() {
    $.ajax({
        type: "POST",
        url: "/Poker/GetAllUsers",
        success: function (response) {
            console.log(response)
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

function loadAnnouncements() {
    location.reload()
}

function getAllAnnouncements() {
    $.ajax({
        type: "POST",
        url: "/Poker/GetAllAnnouncements",
        success: function (response) {
            console.log(response)

            let table = document.getElementById("announcements-table-id")

            for (i = 0; i < response.length; i++) {
                let announcementBox = document.createElement('div')
                announcementBox.setAttribute('class', 'announcement-row')

                //let titleAuthorCategoryColumn = document.createElement('div')
                //titleAuthorCategoryColumn.setAttribute('class', 'announcement-column')

                let titleBox = document.createElement('div')
                titleBox.textContent = response[i].title
                titleBox.setAttribute('class', 'announcement-inner-row-content')

                let authorBox = document.createElement('div')
                authorBox.textContent = response[i].author
                authorBox.setAttribute('class', 'announcement-inner-row-content')

                let categoryBox = document.createElement('div')
                categoryBox.textContent = response[i].category
                categoryBox.setAttribute('class', 'announcement-inner-row-content')

                //titleAuthorCategoryColumn.appendChild(titleBox)
                //titleAuthorCategoryColumn.appendChild(authorBox)
                //titleAuthorCategoryColumn.appendChild(categoryBox)


                //let dateCityColumn = document.createElement('div')
                //dateCityColumn.setAttribute('class', 'announcement-column')

                let dateBox = document.createElement('div')
                dateBox.textContent = response[i].date
                dateBox.setAttribute('class', 'announcement-inner-row-content')

                let cityBox = document.createElement('div')
                cityBox.textContent = response[i].city
                cityBox.setAttribute('class', 'announcement-inner-row-content')

                //dateCityColumn.appendChild(dateBox)
                //dateCityColumn.appendChild(cityBox)

                let emptyColumn = document.createElement('div')
                emptyColumn.setAttribute('class', 'announcement-column')

                let announcementInnerRow1 = document.createElement('div')
                announcementInnerRow1.setAttribute('class', 'announcement-inner-row')

                //announcementInnerRow1.appendChild(titleAuthorCategoryColumn)
                //announcementInnerRow1.appendChild(dateCityColumn)
                //announcementInnerRow1.appendChild(emptyColumn)
                announcementInnerRow1.appendChild(titleBox);
                announcementInnerRow1.appendChild(authorBox);
                announcementInnerRow1.appendChild(categoryBox);
                announcementInnerRow1.appendChild(cityBox);
                announcementInnerRow1.appendChild(dateBox);

                let announcementInnerRow2 = document.createElement('div')
                announcementInnerRow2.setAttribute('class', 'announcement-inner-row')

                let descriptionColumn = document.createElement('div')
                descriptionColumn.setAttribute('class', 'announcement-column')
                descriptionColumn.textContent = "Description: " + response[i].description

                announcementInnerRow2.appendChild(descriptionColumn)

                announcementBox.appendChild(announcementInnerRow1)
                announcementBox.appendChild(announcementInnerRow2)

                table.appendChild(announcementBox)
            }
        },
        error: function (response) {
            console.log("Coś poszło nie tak" + response)
        }
    })
}

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
