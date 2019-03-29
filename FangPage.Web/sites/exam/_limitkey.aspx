<script type="text/javascript">
    if (window.Event)
        function nocontextmenu(e) {
        var ev = e ? e : window.event;
        ev.cancelBubble = true
        ev.returnValue = false;
        if (ev.preventDefault) {
            ev.preventDefault();
        }
        if (ev.stopPropagation) {
            ev.stopPropagation();
        }
        return false;
    }
    function nocopy(e) {
        var ev = e ? e : window.event;
        ev.cancelBubble = true
        ev.returnValue = false;
        if (ev.preventDefault) {
            ev.preventDefault();
        }
        if (ev.stopPropagation) {
            ev.stopPropagation();
        }
        return false;
    }
    function norightclick(e) {
        if (window.Event) {
            if (e.which == 2 || e.which == 3)
                return false;
        }
        else
            if (event.button == 2 || event.button == 3) {
                event.cancelBubble = true;
                event.returnvalue = false;
                return false;
            }

    }

    document.oncontextmenu = nocontextmenu; // for IE5+ 
    document.oncopy = nocopy;
    document.onkeydown = function (event) //shield F5  //shift+F10 ctrl+R
    {
        event = event ? event : (window.event ? window.event : null); // ie firefox
        if (event.keyCode == 116 || (event.shiftKey && event.keyCode == 121) || (event.ctrlKey && event.keyCode == 82)) {
            event.keyCode = 0;
            event.cancelBubble = true;
            event.returnValue = false;
            if (event && event.preventDefault)
                event.preventDefault();
            else
                window.event.returnValue = false;
            return false;
        }
    }
</script>