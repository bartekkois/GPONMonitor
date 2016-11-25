var TooltipWhenOverflow = (function () {
    $(document).on('mouseenter', '.onu-list-id, .onu-list-item, .onu-detail-description, .onu-detail-item', function () {
        var $this = $(this);

        if (this.offsetWidth < this.scrollWidth && !$this.attr('title')) {
            $this.attr('title', $this.text());
        }
    });
}());
