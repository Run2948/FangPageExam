<%if ispost %>
<script type="text/javascript">
    layer.msg('${msg}', 0, 1);
    var bar = 0;
    count();
    function count() {
        bar = bar + 4;
        if (bar < 20) {
            setTimeout("count()", 100);
        }
        else {
            window.location.href = "${link}";
        }
    }
</script>
<%/if %>