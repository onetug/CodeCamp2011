function platinumSponsorCarousel_initCallback(carousel) {
	// Disable autoscrolling if the user clicks the prev or next button.
	carousel.buttonNext.bind('click', function () {
		carousel.startAuto(0);
	});

	carousel.buttonPrev.bind('click', function () {
		carousel.startAuto(0);
	});

	// Pause autoscrolling if the user moves with the cursor over the clip.
	carousel.clip.hover(function () {
		carousel.stopAuto();
	}, function () {
		carousel.startAuto();
	});
};

//it's the size of jCarousel clip - a hack to fis this => https://github.com/jsor/jcarousel/issues#issue/114

jQuery(document).ready(function () {
	jQuery('#platinumSponsorCarousel').jcarousel({
		auto: 2
					, wrap: 'last'
					, vertical: 'true'
					, itemFallbackDimension: 200
					, initCallback: platinumSponsorCarousel_initCallback
					, scroll: 1
	});
	jQuery('#newsCarousel').jcarousel({
		auto: 6
					, wrap: 'last'
					, itemFallbackDimension: 400
		//, itemFallbackDimension: 700
					, initCallback: platinumSponsorCarousel_initCallback
					, scroll: 1
	});
	jQuery('#aCarousel').jcarousel({
		auto: 5
					, wrap: 'last'
					, itemFallbackDimension: 300
		// , itemFallbackDimension: 700
					, initCallback: platinumSponsorCarousel_initCallback
					, scroll: 1
	});

});

twitstat.badge.init({ badge_container: "twitstat_badge_350",
	title: "<b><center><h1>#orlandocc</h1></center></b>",
	keywords: "#orlandocc",
	max: 8,
//	border_color: "#434343",
	border_color: "Black",
	header_background: "#003d5e",
	header_font_color: "White",
	content_background_color: "#e1e1e1",
	content_font_color: "#333333",
	link_color: "#307ace",
	width: 450
});

function OpenChild() {
	var ParmA = retvalA.value;
	var ParmB = retvalB.value;
	var ParmC = retvalC.value;
	var MyArgs = new Array(ParmA, ParmB, ParmC);
	var WinSettings = "center:yes;resizable:no;dialogHeight:300px"
	// ALTER BELOW LINE - supply correct URL for Child Form
	MyArgs = window.showModalDialog("~/AttendeePasswordReset.aspx", MyArgs, WinSettings);
	if (MyArgs == null) {
		window.alert("Nothing returned from child. No changes made to input boxes");
	}
	else {
		retvalA.value = MyArgs[0].toString();
		retvalB.value = MyArgs[1].toString();
		retvalC.value = MyArgs[2].toString();
	}
}

function Done() {
	var ParmA = tbParamA.value;
	var ParmB = tbParamB.value;
	var ParmC = tbParamC.value;
	var MyArgs = new Array(ParmA, ParmB, ParmC);
	window.returnValue = MyArgs;
	window.close();
}

function doInit() {
	var ParmA = "Aparm";
	var ParmB = "Bparm";
	var ParmC = "Cparm";
	var MyArgs = new Array(ParmA, ParmB, ParmC);
	MyArgs = window.dialogArguments;
	tbParamA.value = MyArgs[0].toString();
	tbParamB.value = MyArgs[1].toString();
	tbParamC.value = MyArgs[2].toString();
}