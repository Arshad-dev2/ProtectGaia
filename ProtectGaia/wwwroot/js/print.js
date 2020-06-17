function PrintElem() {
    var mywindow = window.open('', 'PRINT', 'height=400,width=600');

    mywindow.document.write('<html><head><title>My Report : EcoMorph</title>');
    mywindow.document.write('</head><body >');
    mywindow.document.write('<img src="/images/header.PNG" alt="EcoMorph"/>');
    mywindow.document.write("<h5>The report documents that <br/>" + sessionStorage.getItem("ÚserName") + "<br />Has reduced ____grams of Carbon from our environment!</h5>");
    mywindow.document.write('</body></html>');

    mywindow.document.close(); // necessary for IE >= 10
    mywindow.focus(); // necessary for IE >= 10*/

    mywindow.print();
    mywindow.close();

    return true;
}
