function MultiListBox(firstListBox,secondListBox,clientID)
{
	this.FirstListBox=firstListBox;
	this.SecondListBox=secondListBox;
	this.ClientID=clientID;

}
MultiListBox.prototype.transferRight=function()
{
	// Move them over
	for (var i=0; i<this.FirstListBox.options.length; i++)
	{
		var o = this.FirstListBox.options[i];
		if (o.selected) 
		{
			if (!hasOptions(this.SecondListBox)) { var index = 0; } else { var index=this.SecondListBox.options.length; }
			this.SecondListBox.options[index] = new Option( o.text, o.value, false, false);
			this.addItem(o.text,o.value);
		}
	}
	// Delete them from original
	for (var i=(this.FirstListBox.options.length-1); i>=0; i--) {
		var o = this.FirstListBox.options[i];
		if (o.selected) {
			this.FirstListBox.options[i] = null;
		}
	}
	this.FirstListBox.selectedIndex = -1;
	this.SecondListBox.selectedIndex = -1;
	this.oninitStatusImg();
}
MultiListBox.prototype.transferAllLeft=function()
{
	selectAllOptions(this.FirstListBox);
	this.transferRight();
}
MultiListBox.prototype.transferLeft=function()
{
	// Move them over
	for (var i=0; i<this.SecondListBox.options.length; i++)
	{
		var o = this.SecondListBox.options[i];
		if (o.selected) 
		{
			if (!hasOptions(this.SecondListBox)) { var index = 0; } else { var index=this.FirstListBox.options.length; }
			this.FirstListBox.options[index] = new Option( o.text, o.value, false, false);
			this.removeItem(o.text,o.value);
		}
	}
	// Delete them from original
	for (var i=(this.SecondListBox.options.length-1); i>=0; i--) {
		var o = this.SecondListBox.options[i];
		if (o.selected) {
			this.SecondListBox.options[i] = null;
		}
	}
	this.FirstListBox.selectedIndex = -1;
	this.SecondListBox.selectedIndex = -1;
	this.oninitStatusImg();
}
MultiListBox.prototype.transferAllRight=function()
{
	selectAllOptions(this.SecondListBox);
	this.transferLeft();
}
MultiListBox.prototype.oninitStatusImg=function()
{
	var listCount=this.FirstListBox.options.length;
	if (listCount>1){
		$(this.ClientID+"AllRight").src=img_AllRight_have;
		$(this.ClientID+"Right").src=img_Right_have;
	}
	else if (listCount==1){
		$(this.ClientID+"Right").src=img_Right_have;	
		$(this.ClientID+"AllRight").src=img_AllRight_has;	
	}
	else
	{
		$(this.ClientID+"AllRight").src=img_AllRight_has;	
		$(this.ClientID+"Right").src=img_Right_has;	
	}
	listCount=this.SecondListBox.options.length;
	if (listCount>1){
		$(this.ClientID+"AllLeft").src=img_AllLeft_have;
		$(this.ClientID+"Left").src=img_Left_have;
	}
	else if (listCount==1){
		$(this.ClientID+"AllLeft").src=img_AllLeft_has;	
		$(this.ClientID+"Left").src=img_Left_have;	
	}
	else
	{
		$(this.ClientID+"AllLeft").src=img_AllLeft_has;	
		$(this.ClientID+"Left").src=img_Left_has;	
	}
}
MultiListBox.prototype.addItem=function(text,value)
{
	/*var reg=new RegExp(text+"\|"+value+"[,]?");
	var newValue=text+"|"+value;
	var s=$(this.ClientID+"ADDED");
	if (!(reg.test(s.value)))
	{	
		if (s.value.length>0)
			s.value=s.value+","+newValue;
		else
			s.value=newValue;
		//alert(s.value);
	}*/
	var hiddenField = $(this.ClientID + 'ADDED');
	if (hiddenField != null)
	{
		// Add a separator
		var tmp = hiddenField.value;
		if (tmp != '')
		   hiddenField.value += ',';

		// Add the item to the hidden field
		hiddenField.value += text + '|' + value; 
		// if present in the REMOVED hidden field, remove it
		var removeHiddenField = $(this.ClientID + 'REMOVED');
		if (removeHiddenField != null)
		{
			var removedItems = removeHiddenField.value.split(',');
			removeHiddenField.value = '';
			for (var i=0;i<removedItems.length;i++)
			{
				//if (value != removedItems[i])
				//if (removedItems[i].match(value) != null)
				//{
					// Add a separator
					//var tmp1 = removeHiddenField.value;
					//if (tmp1 != '')
					if ((text + '|' + value)!=removedItems[i])
					{
					    if (removeHiddenField.value!='')
						    removeHiddenField.value += ',';
    					removeHiddenField.value += removedItems[i]; 
					}
				//}
			}		

		}
		//alert(removeHiddenField.value);
	}
	
}
MultiListBox.prototype.removeItem=function(text,value)
{
	 var hiddenField =  $(this.ClientID + 'REMOVED');
	 if (hiddenField != null)
	 {
		 	// Add a separator
			var tmp = hiddenField.value;
			if (tmp != '')
			   hiddenField.value += ',';

			//hiddenField.value += value; 
			 hiddenField.value+=text + '|' + value; 
			
			 // if present in the ADDED hidden field, remove it
	 		var addHiddenField = $(this.ClientID + 'ADDED');
			if (addHiddenField != null)
			{
				var addedItems = addHiddenField.value.split(',');
				addHiddenField.value = '';
				for (var i=0;i<addedItems.length;i++)
				{
					if (addedItems[i].match(value) == null)
					{
						// Add a separator
						
						var tmp1 = addHiddenField.value;
						if (tmp1 != '')
							addHiddenField.value += ',';
		
						addHiddenField.value += addedItems[i]; 
					}
				}		
				//alert(addHiddenField.value);
			}
	}
}
function $() {
  var elements = new Array();

  for (var i = 0; i < arguments.length; i++) {
    var element = arguments[i];
    if (typeof element == 'string')
      element = document.getElementById(element);

    if (arguments.length == 1) 
      return element;

    elements.push(element);
  }

  return elements;
}
function selectAllOptions(obj)
{
	if (!hasOptions(obj)) { return; }
	for (var i=0; i<obj.options.length; i++) 
	{
		obj.options[i].selected = true;
	}
}
function hasOptions(obj) {
	if (obj!=null && obj.options!=null) { return true; }
		return false;
}