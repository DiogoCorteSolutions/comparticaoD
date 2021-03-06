var FCKeditorAPI ;

function InitializeAPI()
{
	var oParentWindow = window.parent ;

	if ( !( FCKeditorAPI = oParentWindow.FCKeditorAPI ) )
	{
		var sScript =
			'window.FCKeditorAPI = {' +
				'Version : "2.6.4.1",' +
				'VersionBuild : "23187",' +
				'Instances : window.FCKeditorAPI && window.FCKeditorAPI.Instances || {},' +

				'GetInstance : function( name )' +
				'{' +
					'return this.Instances[ name ];' +
				'},' +

				'_FormSubmit : function()' +
				'{' +
					'for ( var name in FCKeditorAPI.Instances )' +
					'{' +
						'var oEditor = FCKeditorAPI.Instances[ name ] ;' +
						'if ( oEditor.GetParentForm && oEditor.GetParentForm() == this )' +
							'oEditor.UpdateLinkedField() ;' +
					'}' +
					'this._FCKOriginalSubmit() ;' +
				'},' +

				'_FunctionQueue	: window.FCKeditorAPI && window.FCKeditorAPI._FunctionQueue || {' +
					'Functions : new Array(),' +
					'IsRunning : false,' +

					'Add : function( f )' +
					'{' +
						'this.Functions.push( f );' +
						'if ( !this.IsRunning )' +
							'this.StartNext();' +
					'},' +

					'StartNext : function()' +
					'{' +
						'var aQueue = this.Functions ;' +
						'if ( aQueue.length > 0 )' +
						'{' +
							'this.IsRunning = true;' +
							'aQueue[0].call();' +
						'}' +
						'else ' +
							'this.IsRunning = false;' +
					'},' +

					'Remove : function( f )' +
					'{' +
						'var aQueue = this.Functions;' +
						'var i = 0, fFunc;' +
						'while( (fFunc = aQueue[ i ]) )' +
						'{' +
							'if ( fFunc == f )' +
								'aQueue.splice( i,1 );' +
							'i++ ;' +
						'}' +
						'this.StartNext();' +
					'}' +
				'}' +
			'}' ;

		if ( oParentWindow.execScript )
			oParentWindow.execScript( sScript, 'JavaScript' ) ;
		else
		{
			if ( FCKBrowserInfo.IsGecko10 )
			{
				eval.call( oParentWindow, sScript ) ;
			}
			else if( FCKBrowserInfo.IsAIR )
			{
				FCKAdobeAIR.FCKeditorAPI_Evaluate( oParentWindow, sScript ) ;
			}
			else if ( FCKBrowserInfo.IsSafari )
			{
				var oParentDocument = oParentWindow.document ;
				var eScript = oParentDocument.createElement('script') ;
				eScript.appendChild( oParentDocument.createTextNode( sScript ) ) ;
				oParentDocument.documentElement.appendChild( eScript ) ;
			}
			else
				oParentWindow.eval( sScript ) ;
		}

		FCKeditorAPI = oParentWindow.FCKeditorAPI ;

		FCKeditorAPI.__Instances = FCKeditorAPI.Instances ;
	}

	FCKeditorAPI.Instances[ FCK.Name ] = FCK ;
}

function _AttachFormSubmitToAPI()
{
	var oForm = FCK.GetParentForm() ;

	if ( oForm )
	{
		FCKTools.AddEventListener( oForm, 'submit', FCK.UpdateLinkedField ) ;

		if ( !oForm._FCKOriginalSubmit && ( typeof( oForm.submit ) == 'function' || ( !oForm.submit.tagName && !oForm.submit.length ) ) )
		{
			oForm._FCKOriginalSubmit = oForm.submit ;

			oForm.submit = FCKeditorAPI._FormSubmit ;
		}
	}
}

function FCKeditorAPI_Cleanup()
{
	if ( window.FCKConfig && FCKConfig.MsWebBrowserControlCompat
			&& !window.FCKUnloadFlag )
		return ;
	delete FCKeditorAPI.Instances[ FCK.Name ] ;
}
function FCKeditorAPI_ConfirmCleanup()
{
	if ( window.FCKConfig && FCKConfig.MsWebBrowserControlCompat )
		window.FCKUnloadFlag = true ;
}
FCKTools.AddEventListener( window, 'unload', FCKeditorAPI_Cleanup ) ;
FCKTools.AddEventListener( window, 'beforeunload', FCKeditorAPI_ConfirmCleanup) ;