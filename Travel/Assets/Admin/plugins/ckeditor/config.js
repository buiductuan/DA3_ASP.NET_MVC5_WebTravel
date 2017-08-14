/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';
    config.syntaxhighlight_lang='csharp';
    config.language = 'vi';
    config.filebrowerBrowerUrl = '/Assets/Admin/plugins/ckfinder/ckfinder.html';
    config.filebrowerImageBrowerUrl = '/Assets/Admin/plugins/ckfinder/ckfinder.html?Type=Images';
    config.filebrowerFlashBrowerUrl = '/Assets/Admin/plugins/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowerImageUploadUrl = '/images/';
    
    CKFinder.setupCKEditor(null, '/Assets/Admin/plugins/ckfinder/');
};
