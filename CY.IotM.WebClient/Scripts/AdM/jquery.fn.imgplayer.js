
/**
 * playImgs jQuery plugin
 *
 * Copyright (c) 2009 Xing,Xiudong
 * 
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * Released under the MIT, BSD, and GPL Licenses.
 *
 * @author Xing,Xiudong  xingxiudong@gmail.com | http://blog.csdn.net/xxd851116
 * @since: 2009-08-06
 * @version 1.0 2009-08-06
 * @deprecated 1.1 update a problem: the same index li element's show or hide is repeated when mouse over it at 2009-09-23
 * @deprecated 1.2 add row($title_bar.width($container.width() - 5 * 2);) to remove '$title_bar' left-padding and right-padding at 2010-02-24
 */
(function($){
	$.fn.playImgs = function(settings){
		var options = $.extend({
			imgCSS		: {}, // �û��Զ���ͼƬ��ʽ
			transition	: 0,  // ����ģʽѡ�� 1:�ܽ⣬2:����ģʽ��3:����(������),4:����(���ϵ���),5:����(���µ���),6:����(���ҵ���),23:���
			width		: '', // ������div�����Ŀ��
			height		: '', // ������div�����ĸ߶�
			time		: 0,  // ͼƬ���ż�϶ʱ��,��λ������
			duration	: 500,// ͼƬ����ʱ��,��λ������
			onStart		: function(){}, // ��ʼ����ʱִ�еĺ���
			onStop		: function(){}, // ֹͣ����ʱִ�еĺ���
			onShow		: function($) {$.show();}, // ÿҳͼƬ��ʾʱִ�еĺ���
			onHide		: function($) {$.hide();}  // ÿҳͼƬ����ʱִ�еĺ���
		}, settings);

		var __defaultCSSDiv = {'border':'1px solid #CCC','padding':'1px','align':'center','position':'relative','overflow':'hidden','width':options.width,'height':options.height};
		var __defaultCSSImg = {'position':'absolute','z-index':'0',"border":"none"};
		var __defaultCSSUL  = {'margin':'0','padding':'0','fontSize':'12px','z-index':'2','position':'absolute','bottom':'2px','right':'2px','list-style-type':'none'};
		var __defaultCSSLI  = {'float':'left','border':'1px solid #ccc','padding':'2px','width':'15px','text-align':'center','margin-left':'2px','cursor':'pointer','background-color':'#eee'};
		var __defaultCSSTitle= {'background-color':'#eee','position':'absolute','z-index':'1','line-height':'25px', 'font-weight':'700','font-size':'12px','padding':'0 5px','bottom':'0'};

		// ���ͼƬ����������
		var $container 	= this.hide().css(__defaultCSSDiv);
		// ���ͼƬ����
		var $images 	= $container.find("img").hide().css(options.imgCSS).css(__defaultCSSImg);
		// ���һ�ţ������ң�ͼƬ������
		var lastIndex 	= $images.length - 1;
		// ���һ�η���ͼƬ����������0��ʼ��
		var prevIndex	= lastIndex;
		var index = 0;		// ��¼��ǰ����ֵ, Ĭ��Ϊ��һ��ͼƬ��������0
		var timer;			// ��ʱ��

		// ���ɱ�����
		var $title_bar = $("<div>&nbsp;</div>").appendTo($container).css(__defaultCSSTitle).fadeTo('fast', "0.75");
		$title_bar.width($container.width() - 5 * 2);//��ȥ $title_bar��������padding 5px

		// �����������
		var $indexGroups = $("<ul></ul>").css(__defaultCSSUL).fadeTo('fast', 0.9);
		for (var i = 0; i < $images.length; i++) {
			$indexGroups.append("<li>" + (i + 1) + "</li>");
		}
		// ��ȡ���Ԫ�ص�jQuery��������
		var $sn = $indexGroups.appendTo($container).children("li")

		// Ϊÿһ��ͼƬ��hover�¼�
		$images.hover(function(){
			pause();
		},function(){
			timer = setTimeout(run, options.time);
		});

		// Ϊÿһ��li��ǩ��hover�¼�
		$sn.css(__defaultCSSLI).hover(function() {

			// ���㵱ǰͼƬ������
			index = $.trim($(this).text()) - 1;

			if (prevIndex != index) {
				// ������һ��ͼƬ
				hide(prevIndex);

				// ��ʾ��ǰ������ͼƬ
				show(index);
			}
			// alert("prevIndex : " + prevIndex + ",  index : " + index);


			// ��ס��ǰͼƬ����
			prevIndex = index;

			// ��ʱ��ͣ��ͣ����
			pause();
		}, function() {
			prevIndex = $.trim($(this).text()) - 1;
			timer = setTimeout(run, options.time);
		});

		function run() {
			// ����indexֵ�����ָ����ʾ������ʾ��ͼƬ�����ʱ������ʾ��һ��ͼƬ
			index = index == prevIndex ? index + 1 : index;
			// ����indexֵ�������������ֵ������
			index = index > lastIndex ? 0 : index;

			if (options.transition == 23) {
				var random_num = parseInt(Math.random() * 5) + 1;
				$container.playAction(random_num);
			}

			hide(prevIndex);
			show(index);

			prevIndex = index;
			index++;

			timer = setTimeout(run, options.time);
		}

		function show(index) {

			//$("#consle").html("index: " + index + ", prevIndex: " + prevIndex); // ���Դ���

			options.onShow($images.eq(index));
			$title_bar.text($images.eq(index).parent("a").attr("title"));

			$sn.eq(index).css({'background-color':'#ccc','border-color':'#999'});
		}

		function hide(index) {
			options.onHide($images.eq(index));
			$sn.eq(index).css({'background-color':'#eee','border-color':'#ccc'});
		}

		function pause() {
			options.onStop();
			clearTimeout(timer);
		}

		$container.start = function(){ options.onStart();run();return $container;}
		$container.stop  = function(){ options.onStop();pause();return $container;}

		// ���嶯��
		$container.playAction = function(n) {
			switch(n) {
				case 1 :
					options.onShow = function($_) {$_.fadeIn(options.duration);};
					options.onHide = function($_) {$_.fadeOut(options.duration);};
					break;
				case 2 : // ����ģʽ
					options.onShow = function($_) {$_.slideDown(options.duration);};
					options.onHide = function($_) {$_.slideUp(options.duration);};
					break;
				case 3 : // ��
					options.onShow = function($_){
						$_.css("left",  -$_.width() + 'px').show().animate({left: "0px"}, options.duration);
					};
					options.onHide = function($_){
						$_.css("left", '0px').animate({left: $_.width() + 'px'}, options.duration);
					};
					break;
				case 4 : // ��
					options.onShow = function($_){
						$_.css("top", -$_.height() + 'px').show().animate({top: "0px"}, options.duration);
					};
					options.onHide = function($_){
						$_.css("top", '0px').animate({top: $_.height() + 'px'}, options.duration);
					};
					break;
				case 5 : //��
					options.onShow = function($_){
						$_.css("bottom", -$_.height() + 'px').show().animate({bottom: "0px"}, options.duration);
					};
					options.onHide = function($_){
						$_.css("bottom", '0px').animate({bottom: $_.height() + 'px'}, options.duration);
					};
					break;
				case 6 : //��
					options.onShow = function($_){
						$_.css("left",  $_.width() + 'px').show().animate({left: "0px"}, options.duration);
					};
					options.onHide = function($_){
						$_.css("left", '0px').animate({left: -$_.width() + 'px'}, options.duration);
					};
					break;
				case 23 :
					var random = parseInt(Math.random() * 5) + 1;
					break;
			}
			return this;
		}

		return $container.playAction(options.transition).show();
	}
})(jQuery);