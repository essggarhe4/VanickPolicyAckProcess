
$(document).ready(function () {
    SetPendingAccordion();
});

function SetPendingAccordion() {
    $("#vanicktabspending").tabs();
    $("#PolicyPendingAccordion").accordion({
        collapsible: true
    });
    $("#PolicyApproveAccordion").accordion({
        collapsible: true,
        heightStyle: "content"
    });
    
}
    