<%@ Import Namespace="CodeCamp.ASP.Web.UI" %>
<%@ Import Namespace="CodeCamp.ASP.Web.UI.Infrastructure" %>
<%@ Import Namespace="CodeCamp.ASP.Web.UI.Infrastructure.ServiceContracts" %>
<%@ Import Namespace="CodeCamp.ASP.Web.UI.Infrastructure.Services" %>

<!-- These carousels were created from these handy samples. --> 
<!-- http://sorgalla.com/projects/jcarousel/#Dynamic-Content-Loading -->

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>Orlando Code Camp 2011</title>

		<style type="text/css">
		
			.newsCarousel-container-vertical {
				width: 300px;
				height: 300px;
				background: #e8e8e8;
				border: 1px solid #fff;
			}
 
			.newsCarousel-clip-vertical {
				top: 15px;
				width: 290px;
				height: 270px;
				margin: 0 5px;
				z-index: 20;
			}
 
			#newsCarousel li,
			.newsCarousel-item-vertical,
			.newsCarousel-item-placeholder-vertical {
				width: 270px;
				height: auto;
				margin: 5px 0;
			}
 
			.newsCarousel-item h3,
			.newsCarousel-item p {
				margin: 0;
				font-size: 90%;
			}
 
			.newsCarousel-next-vertical {
				position: absolute;
				bottom: 0;
				left: 0;
				width: 300px;
				height: 14px;
				cursor: pointer;
				border-top: 1px solid #fff;
				background: #4088b8 url(images/arrow-down.gif) no-repeat center;
			}
 
			.newsCarousel-next-disabled-vertical {
				cursor: default;
				opacity: .5;
				-moz-opacity: .5;
				filter: alpha(opacity=50);
			}
 
			.newsCarousel-prev-vertical {
				position: absolute;
				top: 0;
				left: 0;
				width: 300px;
				height: 14px;
				cursor: pointer;
				border-bottom: 1px solid #fff;
				background: #4088b8 url(images/arrow-up.gif) no-repeat center;
			}
 
			.newsCarousel-prev-disabled-vertical {
				cursor: default;
				opacity: .5;
				-moz-opacity: .5;
				filter: alpha(opacity=50);
			}
		
		</style>

		<style type="text/css"> 
		
		/* These are the tango skins, separate from the skins in the projects skins/scripts folder. */
		/* This is what puts the borders and margins on the carousel */
		
		   .jcarousel-skin-tango .jcarousel-container {
				-moz-border-radius: 10px;
				background: #F0F6F9;
				border: 1px solid #346F97;
			}

			.jcarousel-skin-tango .jcarousel-direction-rtl {
				direction: rtl;
			}

			.jcarousel-skin-tango .jcarousel-container-horizontal {
				width: 245px;
				padding: 20px 40px;
			}

			.jcarousel-skin-tango .jcarousel-container-vertical {
				width: 75px;
				height: 245px;
				padding: 40px 20px;
			}

			.jcarousel-skin-tango .jcarousel-clip-horizontal {
				width:  245px;
				height: 75px;
			}

			.jcarousel-skin-tango .jcarousel-clip-vertical {
				width:  75px;
				height: 245px;
			}

			.jcarousel-skin-tango .jcarousel-item {
				width: 75px;
				height: 75px;
			}

			.jcarousel-skin-tango .jcarousel-item-horizontal {
				margin-left: 0;
				margin-right: 10px;
			}

			.jcarousel-skin-tango .jcarousel-direction-rtl .jcarousel-item-horizontal {
				margin-left: 10px;
				margin-right: 0;
			}

			.jcarousel-skin-tango .jcarousel-item-vertical {
				margin-bottom: 10px;
			}

			.jcarousel-skin-tango .jcarousel-item-placeholder {
				background: #fff;
				color: #000;
			}

			/**
			 *  Horizontal Buttons
			 */
			.jcarousel-skin-tango .jcarousel-next-horizontal {
				position: absolute;
				top: 43px;
				right: 5px;
				width: 32px;
				height: 32px;
				cursor: pointer;
				background: transparent url(next-horizontal.png) no-repeat 0 0;
			}

			.jcarousel-skin-tango .jcarousel-direction-rtl .jcarousel-next-horizontal {
				left: 5px;
				right: auto;
				background-image: url(prev-horizontal.png);
			}

			.jcarousel-skin-tango .jcarousel-next-horizontal:hover {
				background-position: -32px 0;
			}

			.jcarousel-skin-tango .jcarousel-next-horizontal:active {
				background-position: -64px 0;
			}

			.jcarousel-skin-tango .jcarousel-next-disabled-horizontal,
			.jcarousel-skin-tango .jcarousel-next-disabled-horizontal:hover,
			.jcarousel-skin-tango .jcarousel-next-disabled-horizontal:active {
				cursor: default;
				background-position: -96px 0;
			}

			.jcarousel-skin-tango .jcarousel-prev-horizontal {
				position: absolute;
				top: 43px;
				left: 5px;
				width: 32px;
				height: 32px;
				cursor: pointer;
				background: transparent url(prev-horizontal.png) no-repeat 0 0;
			}

			.jcarousel-skin-tango .jcarousel-direction-rtl .jcarousel-prev-horizontal {
				left: auto;
				right: 5px;
				background-image: url(next-horizontal.png);
			}

			.jcarousel-skin-tango .jcarousel-prev-horizontal:hover {
				background-position: -32px 0;
			}

			.jcarousel-skin-tango .jcarousel-prev-horizontal:active {
				background-position: -64px 0;
			}

			.jcarousel-skin-tango .jcarousel-prev-disabled-horizontal,
			.jcarousel-skin-tango .jcarousel-prev-disabled-horizontal:hover,
			.jcarousel-skin-tango .jcarousel-prev-disabled-horizontal:active {
				cursor: default;
				background-position: -96px 0;
			}

			/**
			 *  Vertical Buttons
			 */
			.jcarousel-skin-tango .jcarousel-next-vertical {
				position: absolute;
				bottom: 5px;
				left: 43px;
				width: 32px;
				height: 32px;
				cursor: pointer;
				background: transparent url(next-vertical.png) no-repeat 0 0;
			}

			.jcarousel-skin-tango .jcarousel-next-vertical:hover {
				background-position: 0 -32px;
			}

			.jcarousel-skin-tango .jcarousel-next-vertical:active {
				background-position: 0 -64px;
			}

			.jcarousel-skin-tango .jcarousel-next-disabled-vertical,
			.jcarousel-skin-tango .jcarousel-next-disabled-vertical:hover,
			.jcarousel-skin-tango .jcarousel-next-disabled-vertical:active {
				cursor: default;
				background-position: 0 -96px;
			}

			.jcarousel-skin-tango .jcarousel-prev-vertical {
				position: absolute;
				top: 5px;
				left: 43px;
				width: 32px;
				height: 32px;
				cursor: pointer;
				background: transparent url(prev-vertical.png) no-repeat 0 0;
			}

			.jcarousel-skin-tango .jcarousel-prev-vertical:hover {
				background-position: 0 -32px;
			}

			.jcarousel-skin-tango .jcarousel-prev-vertical:active {
				background-position: 0 -64px;
			}

			.jcarousel-skin-tango .jcarousel-prev-disabled-vertical,
			.jcarousel-skin-tango .jcarousel-prev-disabled-vertical:hover,
			.jcarousel-skin-tango .jcarousel-prev-disabled-vertical:active {
				cursor: default;
				background-position: 0 -96px;
			}
		
		</style>

		
		<!-- This is the non-tango skin styles used in the other demos -->
		<!-- link rel="stylesheet" type="text/css" href="../../Styles/skin.css" /> -->
		<script type="text/javascript" src="../../Scripts/jquery-1.4.2.min.js"></script>
		<script type="text/javascript" src="../../Scripts/jquery.jcarousel.min.js"></script>
		<script type="text/javascript">
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
					auto: 4
					, wrap: 'last'
					, vertical: 'true'
					, itemFallbackDimension: 100 
					, initCallback: platinumSponsorCarousel_initCallback
			});
				jQuery('#newsCarousel').jcarousel({
					auto: 4
					, wrap: 'last'
					, itemFallbackDimension: 500
					, initCallback: platinumSponsorCarousel_initCallback
					, scroll: 1
				});
		});
		</script>
	</head>


	<body style="border: 0; padding: 0; background: #003d5e url(images/bg.gif) repeat-x; color: #333;">

		<form id="Form1" runat="server">

		<ul id="platinumSponsorCarousel" class="jcarousel-skin-tango">
			<!-- The content goes in here -->
			<li><img src="../../Images/Sponsors/devexpress.gif" 
					style="height: 77px; width: 90px" /></li>
			<li><img src="../../Images/Sponsors/microsoft.gif" 
					style="height: 82px; width: 88px" /></li>
			<li><img src="../../Images/Sponsors/SeminoleStateCollege.jpg" 
					style="height: 75px; width: 88px" /></li>
			<li><img src="../../Images/Sponsors/linxterlogo.jpg" 
					style="height: 69px; width: 86px" /></li>
			<li><img src="../../Images/Sponsors/Infragistics.gif" 
					style="height: 73px; width: 84px" /></li>
		</ul>

		<ul id="newsCarousel" class="jcarousel-skin-tango">
			<!-- The content goes in here -->

			<li>Lorem ipsum dolor sit amet, consectetur nisi timide. Dico timendum considero perspicio philosophiam quam difficultatis Latinis et foris esse initio alter fore apti et dixerim initio. Qua et cum partes subterfugere similes nisi dicitis mihi ea generis, iudiciis litteris mihi partim servare. Quaestio partes totam et possit causa iudicium iam scientiae vehementer vel ut in dicendo bonum vestri perfruique. </li>
			<li> Est ea et essemus natos accommodatior esse invidiosum ipsi adiuvet? Quam animi sunt essemus collegerunt ut alieno et perfruique reprehendat beate posse difficultatis est servare si condemnatus rationem! Magno attingere nisi esse et elucere concitatis facere in hanc quod altera distributionem ingeniis ardentius et agendo clamat.</li>
			<li>Ad altera haec maturius iudicii, quantum et veneficii esse voluptatibus. Re omnes est, animadverteretur certum maximis sua habere praeclaram vim debet debet alieno altera tanti. Virtutes vos ita quae rationem et. Reticendo in esse et consuetudinis condicionem partis dicendo ut facere. Vetere voluptatibus iustituiam scientiae id autem quo timendum eandem quae. Iudicio dicendi directam retentam debet argumento corporis satis vivendi. Qui argumento moderatisque in vivatur laboris, attingere accusatores differunt, notae societatem nec magnificentius et iucunde.</li>
		</ul>
		</form>
	</body>
</html>
