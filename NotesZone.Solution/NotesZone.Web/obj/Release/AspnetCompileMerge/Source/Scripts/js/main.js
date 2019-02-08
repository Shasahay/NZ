if (!window.requestAnimationFrame) {
  window.requestAnimationFrame = (function() {
    return window.webkitRequestAnimationFrame ||
      window.mozRequestAnimationFrame ||
      window.oRequestAnimationFrame ||
      window.msRequestAnimationFrame ||
      function(callback, element) {
        window.setTimeout(callback, 1000 / 60);
      };
  })();
}

document.addEventListener('tizenhwkey', function(e) {
  if (e.keyName === 'back') {
    try {
      tizen.application.getCurrentApplication().exit();
    } catch (error) {}
  }
});

var canvas = document.getElementById('canvas');
var context = canvas.getContext('2d');
var pageElement = document.getElementById('page');

var reachedEdge = false;
var touchStart = null;
var touchDown = false;

var lastTouchTime = 0;
pageElement.addEventListener('touchstart', function(e) {
  touchDown = true;

  if (e.timeStamp - lastTouchTime < 500) {
    lastTouchTime = 0;
    toggleZoom();
  } else {
    lastTouchTime = e.timeStamp;
  }
});

pageElement.addEventListener('touchmove', function(e) {
  if (pageElement.scrollLeft === 0 ||
    pageElement.scrollLeft === pageElement.scrollWidth - page.clientWidth) {
    reachedEdge = true;
  } else {
    reachedEdge = false;
    touchStart = null;
  }

  if (reachedEdge && touchDown) {
    if (touchStart === null) {
      touchStart = e.changedTouches[0].clientX;
    } else {
      var distance = e.changedTouches[0].clientX - touchStart;
      if (distance < -100) {
        touchStart = null;
        reachedEdge = false;
        touchDown = false;
        openNextPage();
      } else if (distance > 100) {
        touchStart = null;
        reachedEdge = false;
        touchDown = false;
        openPrevPage();
      }
    }
  }
});

pageElement.addEventListener('touchend', function(e) {
  touchStart = null;
  touchDown = false;
});

var pdfFile;
var currPageNumber = 1;

var openNextPage = function() {
  var pageNumber = Math.min(pdfFile.numPages, currPageNumber + 1);
  if (pageNumber !== currPageNumber) {
      $('.next').css("display", "block");
      $('.back').css("display", "block");
    currPageNumber = pageNumber;
    openPage(pdfFile, currPageNumber);
  }
  else {
      $('.next').css("display", "none");
  }
};

var openPrevPage = function() {
  var pageNumber = Math.max(1, currPageNumber - 1);
  if (pageNumber !== currPageNumber) {
      $('.back').css("display", "block");
      $('.next').css("display", "block");
    currPageNumber = pageNumber;
    openPage(pdfFile, currPageNumber);
  }
  else {
      $('.back').css("display", "none");
  }
};

var zoomed = false;
var toggleZoom = function () {
  zoomed = !zoomed;
  openPage(pdfFile, currPageNumber);
};

var fitScale = 1;
var openPage = function(pdfFile, pageNumber) {
  var scale = zoomed ? fitScale : 1;

  pdfFile.getPage(pageNumber).then(function(page) {
    viewport = page.getViewport(1);

    if (zoomed) {
      var scale = pageElement.clientWidth / viewport.width;
      viewport = page.getViewport(scale);
    }

    canvas.height = viewport.height;
    canvas.width = viewport.width;

    var renderContext = {
      canvasContext: context,
      viewport: viewport
    };

    page.render(renderContext);
  });
};

PDFJS.disableStream = true;
var openPdf = function(inputpdfFile){
   // PDFJS.getDocument('/files/L2Data.pdf').then(function(pdf) {
    //PDFJS.getDocument('file://C:/Users/Shashank.Sahay/Desktop/L2Data.pdf').then(function (pdf) {
    PDFJS.getDocument(inputpdfFile).then(function (pdf) {
        pdfFile = pdf;
        openPage(pdf, currPageNumber, 1);
    });
};
