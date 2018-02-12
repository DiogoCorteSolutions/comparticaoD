$("a.toggle").click(function () {
        var obj = $(this);
      //  obj.toggleClass("up");
        var selector = obj.attr("href");
        $(selector).slideToggle();
		return false;
    })
	
$(document).ready(function () {
 var slug = window.location.pathname.toUpperCase();
  $("#MainMenu ul li a[href]").each(function () {
		  var h = $(this).attr("href");
		  if (h && typeof (h) === "string" && h != "#") {
			  if (slug.indexOf(h.toUpperCase()) >= 0) {
				  $(this).parents("li.Pai").addClass("zuri_menu_atual");
			  }
		  }
  });
});