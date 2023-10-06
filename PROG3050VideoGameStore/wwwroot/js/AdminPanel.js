$(document).ready(function () {
    console.log("Document is ready."); // This will appear in the console

    // Show the first tab by default
    $(".tab-content:first").show();
    console.log("First tab content shown.");

    // Tab click event
    $(".tab-navigation a").click(function () {
        var tabId = $(this).attr("href");

        console.log("Tab clicked: " + tabId); // This will show the clicked tab ID in the console

        // Hide all tab content
        $(".tab-content").hide();
        console.log("All tab content hidden.");

        // Remove 'active' class from all tabs
        $(".tab-navigation a").removeClass("active");
        console.log("Removed 'active' class from all tabs.");

        // Show the selected tab content
        $(tabId).show();
        console.log("Selected tab content shown: " + tabId);

        // Add 'active' class to the clicked tab
        $(this).addClass("active");
        console.log("Added 'active' class to the clicked tab.");

        return false; // Prevent default anchor behavior
    });
});
