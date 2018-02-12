var FCKSelection = FCK.Selection =
{
	GetParentBlock : function()
	{
		var retval = this.GetParentElement() ;
		while ( retval )
		{
			if ( FCKListsLib.BlockBoundaries[retval.nodeName.toLowerCase()] )
				break ;
			retval = retval.parentNode ;
		}
		return retval ;
	},

	ApplyStyle : function( styleDefinition )
	{
		FCKStyles.ApplyStyle( new FCKStyle( styleDefinition ) ) ;
	}
} ;