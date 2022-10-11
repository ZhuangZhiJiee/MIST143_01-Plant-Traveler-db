// function doClick() {
//   var text = document.querySelectorAll(".card-title");
//   var a = text.value.replace(" ", "").length;
// }
// $(function () {
//   var len = 5; // 超過50個字以"..."取代
//   $(".card-title").each(function (i) {
//     var a = $(this).text().value.replace(" ", "").length;
//     var b = $(this).text().value.replace(" ", "");
//     if (true) {
//       $(this).attr("title", $(this).text());
//       var text = b.substring(0, len - 1) + "...";
//       $(this).text(text);
//     }
//   });
//   console.log(a);
//   console.log(b);
// });
// function lenCount(str) {
//   return str.replace(/[^\x00-\xff]/g, "xx").length;
// }
// $(function () {
//   var len = 28; // 超過50個字以"..."取代
//   $(".card-title").each(function (i) {
//     var a = $(this).text().trim();
//     var b = lenCount(a);
//     // console.log(a);
//     console.log(b);
//     if (b > len) {
//       $(this).attr("title", $(this).text());
//       var text = a.substring(0, len - 1) + "...";
//       console.log(text);
//       $(this).text(text);
//     }
//   });
// });
var swiper = new Swiper(".mySwiper", {
  slidesPerView: 3,
  spaceBetween: 30,
  slidesPerGroup: 3,
  loop: true,
  loopFillGroupWithBlank: true,
  autoplay: {
    delay: 3500,
    disableOnInteraction: false,
  },
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next1",
    prevEl: ".swiper-button-prev1",
  },
});

var swiper = new Swiper(".mySwiper2", {
  slidesPerView: 4,
  spaceBetween: 30,
  slidesPerGroup: 4,
  loop: true,
  loopFillGroupWithBlank: true,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next2",
    prevEl: ".swiper-button-prev2",
  },
});

var swiper = new Swiper(".mySwiper3", {
  slidesPerView: 6,
  spaceBetween: 30,
  slidesPerGroup: 6,
  loop: true,
  loopFillGroupWithBlank: true,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next3",
    prevEl: ".swiper-button-prev3",
  },
});

var swiper = new Swiper(".mySwiper4", {
  slidesPerView: 3,
  spaceBetween: 30,
  slidesPerGroup: 3,
  loop: true,
  loopFillGroupWithBlank: true,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
  navigation: {
    nextEl: ".swiper-button-next4",
    prevEl: ".swiper-button-prev4",
  },
});