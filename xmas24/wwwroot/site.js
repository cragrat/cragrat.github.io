window.newImageWindow = function (element) {
    try {
        var windowFeatures = 'width=600,height=400,top=100,left=100,resizable=yes,scrollbars=yes';
        var newWindow = window.open('about:blank', '_blank', windowFeatures);

        if (newWindow) {

            newWindow.document.write('<html><head><title>Full Image</title></head><body  style="margin: 0; display: flex; justify-content: center; align-items: center; height: 100vh;"><img src="' + element + '" style="max-width: 100%; max-height: 100%;"/></body></html>');
            newWindow.document.close();
        }
        else {
            alert('Popup blocked! Please allow pop-ups for this site to view the image.');
        }
    } catch (error) {
        console.error('An error occurred while loading the image:', error);
    }
};