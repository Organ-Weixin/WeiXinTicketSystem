var App = (function () {
    "use strict"

    var that = {}

    // -===- Utils Begin -===- 

    that.showNoti = function (text, is_success) {
        var message = (text && text !== 'success') ? text : '操作成功',
            className = is_success ? 'success' : 'error'
        $.notify(message, {
            style: 'admin',
            className: className,
            autoHideDelay: 2000
        })
    }

    that.onOperationSuccessSimple = function (response) {
        that.showNoti(response.message, response.code === 0)
        if (response.code === 0) {
            setTimeout(function () {
                if (response.data && response.data.url) {
                    window.location = response.data.url
                } else {
                    window.location.reload()
                }
            }, 600)
        }
    }

    that.OnModalSubmitSuccess = function (response) {
        that.showNoti(response.message, response.code === 0)
        if (response.code === 0) {
            setTimeout(function () {
                that.hideModal()
            }, 600)
        }
    }

    that.onOperationSuccessWithinPagedList = function (response) {
        var dynatable = $('.dataTable').data('dynatable')

        that.showNoti(response.message, response.code === 0)
        if (response.code === 0) {
            dynatable.process()
            that.hideModal()
        }
    }

    that.onFormCommitBegin = function () {
        $('button[type="submit"]').attr('disabled', 'disabled')
    }

    that.onFormCommitComplete = function () {
        $('button[type="submit"]').removeAttr('disabled')
    }

    that.hideModal = function () {
        $('.modal.in').modal('hide')
    }

    // -===- Utils End -===- 

    that.dateRangePickerLocal = {
        "format": "MM/DD/YYYY",
        "separator": " - ",
        "applyLabel": "确定",
        "cancelLabel": "清空",
        "fromLabel": "从",
        "toLabel": "到",
        "customRangeLabel": "自定义",
        "daysOfWeek": [
            "日",
            "一",
            "二",
            "三",
            "四",
            "五",
            "六"
        ],
        "monthNames": [
            "一月",
            "二月",
            "三月",
            "四月",
            "五月",
            "六月",
            "七月",
            "八月",
            "九月",
            "十月",
            "十一月",
            "十二月"
        ],
        "firstDay": 1
    }

    return that
}())

function attachSelect2(container) {
    $(container).find('[data-toggle="select2"]').select2({
        minimumResultsForSearch: Infinity
    })
}

function attachiCheck(container) {
    $(container).find('[data-toggle="iCheck"],[data-toggle=state-switch]').iCheck({
        checkboxClass: 'icheckbox_minimal-grey',
        radioClass: 'iradio_minimal-grey',
        increaseArea: '20%' // optional
    })
}



function attachFileInput(container) {
    $(container).find('[data-toggle="fileinput"]')
                       .fileinput()
                       .on('clear.bs.fileinput', function (e) {
                           $('#' + $(e.target).data('for')).val(null)
                       })
}

function attachTagsInput(container) {
    $.each($(container).find('[data-toggle=tagsinput]'), function () {
        var $this = $(this),
            defaultText = $this.data('default-text')

        $this.tagsInput({
            defaultText: defaultText
        })
    })
}

function attachDatepicker(container) {

    $.each($(container).find('[data-toggle="datepicker"]'), function () {
        var $this = $(this),
        config = {
            format: $this.data('format') || 'yyyy-mm-dd',
            startDate: $this.data('start-date'),
            language: "zh-CN",
            autoclose: $this.data('autoclose'),
        }

        $this.datepicker(config)
    })

}

function attachTimepicker(container) {

    $.each($(container).find('[data-toggle="timepicker"]'), function () {
        var $this = $(this),
        config = {
            showInputs: false,
            // defaultTime: false,
            showMeridian: false
        }

        $this.timepicker(config)
    })
}

function attachDateTimepicker(container) {

    $.each($(container).find('[data-toggle="datetimepicker"]'), function () {
        var $this = $(this),
        config = {
            format: $this.data('format') || 'yyyy-mm-dd HH:mm:ss',
            startDate: $this.data('start-date'),
            language: 'zh-CN',
            weekStart: 1,
            todayBtn: 1,
            autoclose: $this.data('autoclose'),
            todayHighlight: 1,
            forceParse: 0,
            minuteStep: $this.data('minute-step')
        }

        $this.datetimepicker(config)
    })

}

function attachPopover(container) {
    $(container).find('[data-toggle="popover"]').popover({
        trigger: 'click',
        html: true,
        "animation": false,
        content: function () {
            var $this = $(this),
            source = $($this.data('content-template')).html().trim(),
            template = Handlebars.compile(source),
            html = template(this.dataset)

            return html
        }
    })
}

$(function () {
    //select2
    attachSelect2(document)

    //iCheck
    attachiCheck(document)

    //fileinput
    //attachFileInput(document)

    //datepicker
    attachDatepicker(document)

    //timepicker
    attachTimepicker(document)

    //datetimepicker
    attachDateTimepicker(document)

    //popover
    attachPopover(document)

    // modal form
    $('body').on('click', '[data-toggle="modal-form"]', function (event) {
        event.preventDefault()

        $.ajax({
            url: this.href,
            success: function (response) {
                var modal = $(response).appendTo($('body'))
                .on('hidden.bs.modal', function (e) {
                    modal.remove()
                })
                .on('shown.bs.modal', function (e) {

                    setTimeout(function () {
                        $(e.target).find('input[type="text"]:first').focus()
                    }, 200)

                    // validator
                    $.validator.unobtrusive.parse($(e.target))

                    //iCheck
                    attachiCheck(e.target)

                    //select2
                    attachSelect2(e.target)

                    //fileinput
                    //attachFileInput(e.target)

                    //colorpicker
                    //$(e.target).find('[data-toggle=colorpicker]').colorpicker()

                    //datepicker
                    attachDatepicker(e.target)

                    //timepicker
                    attachTimepicker(e.target)

                    //datetimepicker
                    attachDateTimepicker(e.target)

                    //calendar
                    var calendar = $(e.target).find('[data-toggle=calendar]')
                    if (calendar.length > 0) {
                        calendar.fullCalendar(window[calendar.data('config')](calendar))
                    }

                }).modal('show')

            }
        })
    })

    // modal show
    $('body').on('click', '[data-toggle="modal-show"]', function (event) {
        event.preventDefault()

        $.ajax({
            url: this.href,
            success: function (response) {
                var modaal = $(response).appendTo($('body'))
                .on('hidden.bs.modal', function (e) {
                    modal.remove()
                })
                .modal('show')
            }
        })
    })

    // popover
    $('body').on('click', function (e) {
        $('[data-toggle=popover]').each(function () {
            // hide any open popovers when the anywhere else in the body is clicked
            if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                //console.log($(this).data('href'))
                $(this).popover('hide')
            }
        })
    })

    $('body').on('click', '[data-toggle=popover]', function (e) {
        e.preventDefault()

        $(this).popover('toggle')
    })

    $('[data-toggle="dynatable-paged"]').each(function () {
        var $this = $(this)
        var sortable = false;
        if ($this.data('sortby') != undefined) {
            sortable = $this.data('sortby');
        }
        $this
            .bind('dynatable:init', function (e, dynatable) {

                // style pagination
                var $container = $(this).parent(),
                    $links = $container.find('.dynatable-pagination-links'),
                    $record_count = $container.find('.dynatable-record-count'),
                    $per_page = $container.find('.dynatable-per-page'),
                    $custom_filter = $container.find('.dynatable-custom-filter'),
                    $custom_operation = $container.find('.dynatable-operation'),
                    $search = $container.find('.dynatable-search')

                var $pagination_container = $('<div>', { "class": "row" }).appendTo($container)
                $record_count.remove().appendTo($pagination_container).wrap('<div class="col-xs-6"><div class=""></div></div>')
                $links.remove().appendTo($pagination_container).wrap('<div class="col-xs-6"><div class="dataTables_paginate"></div></div>')
                $links.before($per_page)
                $search.wrap('<div class="row"></div>')
                    .before($custom_filter.show())
                    .after($custom_operation.show())
                    .find('input')
                    .addClass('form-control')
                    .text("")
                    .attr('placeholder', $this.data('search-placeholder') || '请输入搜索关键字')

            })
            .bind('dynatable:afterUpdate', function (e, $rows) {

                $this.find('[data-toggle="popover"]').popover({
                    trigger: 'manual',
                    html: true,
                    "animation": false,
                    content: function () {
                        var $this = $(this),
                        source = $($this.data('content-template')).html().trim(),
                        template = Handlebars.compile(source),
                        html = template(this.dataset)

                        return html
                    }
                })



                $this.find('[data-toggle="tooltip"]').tooltip()

                $this.find('[data-toggle=state-switch]').change(function () {
                    var $switchInput = $(this),
                        input = this
                    if (input.dataset.url) {
                        $.post(input.dataset.url, { state: input.checked }, function (data) {
                            if (data.code !== 0) {
                                App.showNoti(data.message, false)
                                input.checked = !input.checked
                                //$(input).bootstrapSwitch('state', input.checked)
                            }
                        })
                        .error(function () {
                            App.showNoti('发送网络错误', false)
                            input.checked = !input.checked
                            //$(input).bootstrapSwitch('state', input.checked)
                        })
                    }
                })

            })
            .bind('dynatable:beforeProcess', function (data) {
                $('[data-toggle=select2]').trigger('change.select2')
            })
            .dynatable({
                dataset: {
                    ajax: true,
                    ajaxUrl: $this.data('url'),
                    ajaxOnLoad: true,
                    records: []
                },
                features: {
                    sort: false,
                    paginate: $this.data('feature-paginate') == undefined ? true : $this.data('feature-paginate'),
                },
                inputs: {
                    perPageText: '每页显示 ',
                    processingText: '正在加载...',
                    paginationPrev: '<<',
                    paginationNext: '>>', 
                    searchText: '', 
                    recordCountText: '显示',
                    recordCountPageBoundTemplate: '{pageLowerBound} 到 {pageUpperBound}    共',
                    recordCountPageUnboundedTemplate: '{recordsShown}    共',
                    recordCountTotalTemplate: '{recordsQueryCount} 条记录',
                    queries: $($this.parent().find('.dynatable-query'))
                },
                writers: {
                    _rowWriter: function (rowIndex, record, columns, cellWriter) {
                        var source = $('#' + $this.data('row-template')).html().trim(),
                            template = Handlebars.compile(source),
                            html = template(record)

                        return html
                    }
                }
            })
        //sortby
        var d = $this.data('dynatable');
        if (sortable) {
            $this.find('tbody').sortable({
                cancel: 'tr td:not(:first-child)',
                update: function (event, ui) {
                    var id = $(ui.item).data('sortable-id');
                    var oldPosition = 0, newPosition = 0, replaceid = 0;
                    var filedName = $(ui.item).data('sortable-name') || 'id'

                    var records = d.settings.dataset.records;
                    $.each(records, function (i, record) {
                        if (record[filedName] === id) {
                            oldPosition = i;
                            return false;
                        }
                    });
                    var trs = d.$element.find('> tbody > tr');
                    $.each(trs, function (i, tr) {
                        var _id = $(tr).data('sortable-id');
                        if (_id === id) {
                            newPosition = i;
                            var up = (newPosition - oldPosition) < 0;
                            replaceid = up ? $(trs[i + 1]).data('sortable-id') : $(trs[i - 1]).data('sortable-id');
                            return false;
                        }
                    });

                    $.ajax({
                        url: d.$element.data('sortable-ajax-url'),
                        type: 'POST',
                        data: { id: id, step: newPosition - oldPosition, replaceid: replaceid },
                        success: function () {
                            window.location.reload();

                        }
                    });
                }
            });
        }
    })
})