Date.dayNames = ["Domingo", "Segunda-feira", "Terça-feira", "Quarta-feira", "Quinta-feira", "Sexta-feira", "Sábado"];
Date.abbrDayNames = ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sáb"];
Date.monthNames = ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
Date.abbrMonthNames = ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];
Date.firstDayOfWeek = 0;
Date.format = "dd/mm/yyyy";
Date.fullYearStart = "20";
(function() {
    function b(c, e) {
        if (!Date.prototype[c]) {
            Date.prototype[c] = e;
        }
    }
    b("isLeapYear", function() {
        var c = this.getFullYear();
        return (c % 4 == 0 && c % 100 != 0) || c % 400 == 0;
    });
    b("isWeekend", function() {
        return this.getDay() == 0 || this.getDay() == 6;
    });
    b("isWeekDay", function() {
        return !this.isWeekend();
    });
    b("getDaysInMonth", function() {
        return [31, (this.isLeapYear() ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][this.getMonth()];
    });
    b("getDayName", function(c) {
        return c ? Date.abbrDayNames[this.getDay()] : Date.dayNames[this.getDay()];
    });
    b("getMonthName", function(c) {
        return c ? Date.abbrMonthNames[this.getMonth()] : Date.monthNames[this.getMonth()];
    });
    b("getDayOfYear", function() {
        var c = new Date("1/1/" + this.getFullYear());
        return Math.floor((this.getTime() - c.getTime()) / 86400000);
    });
    b("getWeekOfYear", function() {
        return Math.ceil(this.getDayOfYear() / 7);
    });
    b("setDayOfYear", function(c) {
        this.setMonth(0);
        this.setDate(c);
        return this;
    });
    b("addYears", function(c) {
        this.setFullYear(this.getFullYear() + c);
        return this;
    });
    b("addMonths", function(e) {
        var c = this.getDate();
        this.setMonth(this.getMonth() + e);
        if (c > this.getDate()) {
            this.addDays(-this.getDate());
        }
        return this;
    });
    b("addDays", function(c) {
        this.setTime(this.getTime() + (c * 86400000));
        return this;
    });
    b("addHours", function(c) {
        this.setHours(this.getHours() + c);
        return this;
    });
    b("addMinutes", function(c) {
        this.setMinutes(this.getMinutes() + c);
        return this;
    });
    b("addSeconds", function(c) {
        this.setSeconds(this.getSeconds() + c);
        return this;
    });
    b("zeroTime", function() {
        this.setMilliseconds(0);
        this.setSeconds(0);
        this.setMinutes(0);
        this.setHours(0);
        return this;
    });
    b("asString", function(e) {
        var c = e || Date.format;
        return c.split("yyyy").join(this.getFullYear()).split("yy").join((this.getFullYear() + "").substring(2)).split("mmmm").join(this.getMonthName(false)).split("mmm").join(this.getMonthName(true)).split("mm").join(a(this.getMonth() + 1)).split("dd").join(a(this.getDate())).split("hh").join(a(this.getHours())).split("min").join(a(this.getMinutes())).split("ss").join(a(this.getSeconds()));
    });
    Date.fromString = function(o, n) {
        var j = n || Date.format;
        var m = new Date("01/01/1977");
        var k = 0;
        var c = j.indexOf("mmmm");
        if (c > -1) {
            for (var g = 0; g < Date.monthNames.length; g++) {
                var e = o.substr(c, Date.monthNames[g].length);
                if (Date.monthNames[g] == e) {
                    k = Date.monthNames[g].length - 4;
                    break;
                }
            }
            m.setMonth(g);
        } else {
            c = j.indexOf("mmm");
            if (c > -1) {
                var e = o.substr(c, 3);
                for (var g = 0; g < Date.abbrMonthNames.length; g++) {
                    if (Date.abbrMonthNames[g] == e) {
                        break;
                    }
                }
                m.setMonth(g);
            } else {
                m.setMonth(Number(o.substr(j.indexOf("mm"), 2)) - 1);
            }
        }
        var l = j.indexOf("yyyy");
        if (l > -1) {
            if (c < l) {
                l += k;
            }
            m.setFullYear(Number(o.substr(l, 4)));
        } else {
            if (c < l) {
                l += k;
            }
            m.setFullYear(Number(Date.fullYearStart + o.substr(j.indexOf("yy"), 2)));
        }
        var h = j.indexOf("dd");
        if (c < h) {
            h += k;
        }
        m.setDate(Number(o.substr(h, 2)));
        if (isNaN(m.getTime())) {
            return false;
        }
        return m;
    };
    var a = function(c) {
        var e = "0" + c;
        return e.substring(e.length - 2);
    };
})();
(function(e) {
    e.fn.bExt({
        renderCalendar: function(v) {
            var E = function(r) {
                return document.createElement(r);
            };
            v = e.bExt({}, e.fn.datePicker.defaults, v);
            if (v.showHeader != e.dpConst.SHOW_HEADER_NONE) {
                var q = e(E("tr"));
                for (var z = Date.firstDayOfWeek; z < Date.firstDayOfWeek + 7; z++) {
                    var j = z % 7,
                        y = Date.dayNames[j],
                        n = Date.abbrDayNames[j],
                        A = v.showHeader == e.dpConst.SHOW_HEADER_SHORT ? n.substr(0, 1) : n;
                    q.append(jBDNsc(E("th")).attr({
                        scope: "col",
                        abbr: n,
                        title: y,
                        "class": (j == 0 || j == 6 ? "weekend" : "weekday")
                    }).html('<a href="javascript:;" title="' + y + '">' + A + "</a>"));
                }
            }
            var f = e(E("table")).addClass("jCalendar").attr("data-ag2-calendar-day-selected", (new Date).asString("mm/dd/yyyy")).append((v.showHeader != e.dpConst.SHOW_HEADER_NONE ? e(E("thead")).append(q) : E("thead")));
            var g = e(E("tbody"));
            var B = (new Date()).zeroTime();
            B.setHours(12);
            var D = v.month == undefined ? B.getMonth() : v.month;
            var t = v.year || B.getFullYear();
            var o = (new Date(t, D, 1, 12, 0, 0));
            var m = Date.firstDayOfWeek - o.getDay() + 1;
            if (m > 1) {
                m -= 7;
            }
            var u = Math.ceil(((-1 * m + 1) + o.getDaysInMonth()) / 7);
            o.addDays(m - 1);
            var C = function(r) {
                return function() {
                    if (v.hoverClass) {
                        var s = e(this);
                        if (!v.selectWeek) {
                            s.addClass(v.hoverClass);
                        } else {
                            if (r && !s.is(".disabled")) {
                                s.parent().addClass("activeWeekHover");
                            }
                        }
                    }
                };
            };
            var h = function() {
                if (v.hoverClass) {
                    var r = e(this);
                    r.removeClass(v.hoverClass);
                    r.parent().removeClass("activeWeekHover");
                }
            };
            var p = 0;
            while (p++ < u) {
                var x = jBDNsc(E("tr")),
                    l = v.dpController ? o > v.dpController.startDate : false;
                for (var z = 0; z < 7; z++) {
                    var k = o.getMonth() == D;
                    d = e(E("td")).html('<a href="javascript:;" class="label-dia" title="Dia ' + o.getDate() + '">' + o.getDate() + "</a>").addClass((k ? "current-month " : "other-month ") + (o.isWeekend() ? "weekend " : "weekday ") + (k && o.getTime() == B.getTime() ? "today " : "")).data("datePickerDate", o.asString()).hover(C(l), h);
                    x.append(d);
                    if (v.renderCallback) {
                        v.renderCallback(d, o, D, t);
                    }
                    o = new Date(o.getFullYear(), o.getMonth(), o.getDate() + 1, 12, 0, 0);
                }
                g.append(x);
            }
            f.append(g);
            return this.bdneach(function() {
                e(this).empty().append(f);
            });
        },
        datePicker: function(f) {
            if (!e.event._dpCache) {
                e.event._dpCache = [];
            }
            f = e.bExt({}, e.fn.datePicker.defaults, f);
            return this.bdneach(function() {
                var h = e(this);
                var k = true;
                if (!this._dpId) {
                    this._dpId = e.guid++;
                    e.event._dpCache[this._dpId] = new a(this);
                    k = false;
                }
                if (f.inline) {
                    f.createButton = false;
                    f.displayClose = false;
                    f.closeOnSelect = false;
                    h.empty();
                }
                var g = e.event._dpCache[this._dpId];
                g.init(f);
                if (!k && f.createButton) {
                    g.button = e('<a href="#" class="dp-choose-date" title="' + e.dpText.TEXT_CHOOSE_DATE + '">' + e.dpText.TEXT_CHOOSE_DATE + "</a>").bind("click", function() {
                        h.dpDisplay(this);
                        this.blur();
                        return false;
                    });
                    h.after(g.button);
                }
                if (!k && h.is(":text")) {
                    h.bind("dateSelected", function(m, l, n) {
                        this.value = l.asString();
                    }).bind("change", function() {
                        if (this.value == "") {
                            g.clearSelected();
                        } else {
                            var l = Date.fromString(this.value);
                            if (l) {
                                g.setSelected(l, true, true);
                            }
                        }
                    });
                    if (f.clickInput) {
                        h.bind("click", function() {
                            h.trigger("change");
                            h.dpDisplay();
                        });
                    }
                    var j = Date.fromString(this.value);
                    if (this.value != "" && j) {
                        g.setSelected(j, true, true);
                    }
                }
                h.addClass("dp-applied");
            });
        },
        dpSetDisabled: function(f) {
            return b.call(this, "setDisabled", f);
        },
        dpSetStartDate: function(f) {
            return b.call(this, "setStartDate", f);
        },
        dpSetEndDate: function(f) {
            return b.call(this, "setEndDate", f);
        },
        dpGetSelected: function() {
            var f = c(this[0]);
            if (f) {
                return f.getSelected();
            }
            return null;
        },
        dpSetSelected: function(j, g, f, h) {
            if (g == undefined) {
                g = true;
            }
            if (f == undefined) {
                f = true;
            }
            if (h == undefined) {
                h = true;
            }
            return b.call(this, "setSelected", Date.fromString(j), g, f, h);
        },
        dpSetDisplayedMonth: function(f, g) {
            return b.call(this, "setDisplayedMonth", Number(f), Number(g), true);
        },
        dpDisplay: function(f) {
            return b.call(this, "display", f);
        },
        dpSetRenderCallback: function(f) {
            return b.call(this, "setRenderCallback", f);
        },
        dpSetPosition: function(f, g) {
            return b.call(this, "setPosition", f, g);
        },
        dpSetOffset: function(f, g) {
            return b.call(this, "setOffset", f, g);
        },
        dpClose: function() {
            return b.call(this, "_closeCalendar", false, this[0]);
        },
        dpRerenderCalendar: function() {
            return b.call(this, "_rerenderCalendar");
        },
        _dpDestroy: function() {}
    });
    var b = function(j, h, g, l, k) {
        return this.bdneach(function() {
            var f = c(this);
            if (f) {
                f[j](h, g, l, k);
            }
        });
    };

    function a(f) {
        this.ele = f;
        this.displayedMonth = null;
        this.displayedYear = null;
        this.startDate = null;
        this.endDate = null;
        this.showYearNavigation = null;
        this.closeOnSelect = null;
        this.displayClose = null;
        this.rememberViewedMonth = null;
        this.selectMultiple = null;
        this.numSelectable = null;
        this.numSelected = null;
        this.verticalPosition = null;
        this.horizontalPosition = null;
        this.verticalOffset = null;
        this.horizontalOffset = null;
        this.button = null;
        this.renderCallback = [];
        this.selectedDates = {};
        this.inline = null;
        this.context = "#dp-popup";
        this.settings = {};
    }
    e.bExt(a.prototype, {
        init: function(f) {
            this.setStartDate(f.startDate);
            this.setEndDate(f.endDate);
            this.setDisplayedMonth(Number(f.month), Number(f.year));
            this.setRenderCallback(f.renderCallback);
            this.showYearNavigation = f.showYearNavigation;
            this.closeOnSelect = f.closeOnSelect;
            this.displayClose = f.displayClose;
            this.rememberViewedMonth = f.rememberViewedMonth;
            this.selectMultiple = f.selectMultiple;
            this.numSelectable = f.selectMultiple ? f.numSelectable : 1;
            this.numSelected = 0;
            this.verticalPosition = f.verticalPosition;
            this.horizontalPosition = f.horizontalPosition;
            this.hoverClass = f.hoverClass;
            this.setOffset(f.verticalOffset, f.horizontalOffset);
            this.inline = f.inline;
            this.settings = f;
            if (this.inline) {
                this.context = this.ele;
                this.display();
            }
        },
        setStartDate: function(f) {
            if (f) {
                if (f instanceof Date) {
                    this.startDate = f;
                } else {
                    this.startDate = Date.fromString(f);
                }
            }
            if (!this.startDate) {
                this.startDate = (new Date()).zeroTime();
            }
            this.setDisplayedMonth(this.displayedMonth, this.displayedYear);
        },
        setEndDate: function(f) {
            if (f) {
                if (f instanceof Date) {
                    this.endDate = f;
                } else {
                    this.endDate = Date.fromString(f);
                }
            }
            if (!this.endDate) {
                this.endDate = (new Date("12/31/2999"));
            }
            if (this.endDate.getTime() < this.startDate.getTime()) {
                this.endDate = this.startDate;
            }
            this.setDisplayedMonth(this.displayedMonth, this.displayedYear);
        },
        setPosition: function(f, g) {
            this.verticalPosition = f;
            this.horizontalPosition = g;
        },
        setOffset: function(f, g) {
            this.verticalOffset = parseInt(f) || 0;
            this.horizontalOffset = parseInt(g) || 0;
        },
        setDisabled: function(f) {
            $e = e(this.ele);
            $e[f ? "addClass" : "removeClass"]("dp-disabled");
            if (this.button) {
                $but = e(this.button);
                $but[f ? "addClass" : "removeClass"]("dp-disabled");
                $but.attr("title", f ? "" : e.dpText.TEXT_CHOOSE_DATE);
            }
            if ($e.is(":text")) {
                $e.attr("disabled", f ? "disabled" : "");
            }
        },
        setDisplayedMonth: function(f, o, k) {
            if (this.startDate == undefined || this.endDate == undefined) {
                return;
            }
            var j = new Date(this.startDate.getTime());
            j.setDate(1);
            var n = new Date(this.endDate.getTime());
            n.setDate(1);
            var h;
            if ((!f && !o) || (isNaN(f) && isNaN(o))) {
                h = new Date().zeroTime();
                h.setDate(1);
            } else {
                if (isNaN(f)) {
                    h = new Date(o, this.displayedMonth, 1);
                } else {
                    if (isNaN(o)) {
                        h = new Date(this.displayedYear, f, 1);
                    } else {
                        h = new Date(o, f, 1);
                    }
                }
            }
            if (h.getTime() < j.getTime()) {
                h = j;
            } else {
                if (h.getTime() > n.getTime()) {
                    h = n;
                }
            }
            var g = this.displayedMonth;
            var l = this.displayedYear;
            this.displayedMonth = h.getMonth();
            this.displayedYear = h.getFullYear();
            if (k && (this.displayedMonth != g || this.displayedYear != l)) {
                this._rerenderCalendar();
                e(this.ele).trigger("dpMonthChanged", [this.displayedMonth, this.displayedYear]);
            }
        },
        setSelected: function(n, f, g, j) {
            if (n < this.startDate || n.zeroTime() > this.endDate.zeroTime()) {
                return;
            }
            var h = this.settings;
            if (h.selectWeek) {
                n = n.addDays(-(n.getDay() - Date.firstDayOfWeek + 7) % 7);
                if (n < this.startDate) {
                    return;
                }
            }
            if (f == this.isSelected(n)) {
                return;
            }
            if (this.selectMultiple == false) {
                this.clearSelected();
            } else {
                if (f && this.numSelected == this.numSelectable) {
                    return;
                }
            }
            if (g && (this.displayedMonth != n.getMonth() || this.displayedYear != n.getFullYear())) {
                this.setDisplayedMonth(n.getMonth(), n.getFullYear(), true);
            }
            this.selectedDates[n.asString()] = f;
            this.numSelected += f ? 1 : -1;
            var l = "td." + (n.getMonth() == this.displayedMonth ? "current-month" : "other-month");
            var m;
            e(l, this.context).bdneach(function() {
                if (e(this).data("datePickerDate") == n.asString()) {
                    m = e(this);
                    if (h.selectWeek) {
                        m.parent()[f ? "addClass" : "removeClass"]("selectedWeek");
                    }
                    m[f ? "addClass" : "removeClass"]("selected");
                }
            });
            e("td", this.context).not(".selected")[this.selectMultiple && this.numSelected == this.numSelectable ? "addClass" : "removeClass"]("unselectable");
            if (j) {
                var h = this.isSelected(n);
                $e = e(this.ele);
                var k = Date.fromString(n.asString());
                $e.trigger("dateSelected", [k, m, h]);
                $e.trigger("change");
            }
        },
        isSelected: function(f) {
            return this.selectedDates[f.asString()];
        },
        getSelected: function() {
            var g = [];
            for (var f in this.selectedDates) {
                if (this.selectedDates[f] == true) {
                    g.push(Date.fromString(f));
                }
            }
            return g;
        },
        clearSelected: function() {
            this.selectedDates = {};
            this.numSelected = 0;
            e("td.selected", this.context).removeClass("selected").parent().removeClass("selectedWeek");
        },
        display: function(g) {
            if (e(this.ele).is(".dp-disabled")) {
                return;
            }
            g = g || this.ele;
            var t = this;
            var o = e(g);
            var r = o.offset();
            var u;
            var v;
            var j;
            var p;
            if (t.inline) {
                u = e(this.ele);
                v = {
                    id: "calendar-" + this.ele._dpId,
                    "class": "dp-popup dp-popup-inline"
                };
                e(".dp-popup", u).remove();
                p = {};
            } else {
                u = e("body");
                v = {
                    id: "dp-popup",
                    "class": "dp-popup"
                };
                p = {
                    top: r.top + t.verticalOffset,
                    left: r.left + t.horizontalOffset
                };
                var q = function(y) {
                    var w = y.target;
                    var x = e("#dp-popup")[0];
                    while (true) {
                        if (w == x) {
                            return true;
                        } else {
                            if (w == document) {
                                t._closeCalendar();
                                return false;
                            } else {
                                w = e(w).parent()[0];
                            }
                        }
                    }
                };
                this._checkMouse = q;
                t._closeCalendar(true);
                e(document).bind("keydown.datepicker", function(w) {
                    if (w.keyCode == 27) {
                        t._closeCalendar();
                    }
                });
            }
            if (!t.rememberViewedMonth) {
                var n = this.getSelected()[0];
                if (n) {
                    n = new Date(n);
                    this.setDisplayedMonth(n.getMonth(), n.getFullYear(), false);
                }
            }
            var m = e(document.createElement("select")).addClass("combo-month"),
                l = e(document.createElement("select")).addClass("combo-year"),
                f = 0,
                s = this.endDate.getFullYear(),
                dspYear = this.displayedYear,
                limit = s - 2010,
                k = this;
            m.add(l).bind("change", function(w) {
                w.preventDefault();
                k.setDisplayedMonth(window.parseInt(m.val(), 10), window.parseInt(l.val(), 10), true);
                m.children().removeAttr("selected").filter("[value='" + k.displayedMonth + "']").attr("selected", "selected");
                l.children().removeAttr("selected").filter("[value='" + k.displayedYear + "']").attr("selected", "selected");
            });
            for (; f > 0; f--) {
                l.append(e("<option"+ (s + f == dspYear ? ' selected ':' ') +"value=" + (s + f) + ">" + (s + f) + "</option>"));
            }
            l.append(e("<option"+ (s == dspYear ? ' selected ':' ') +"value=" + s + ">" + s + "</option>"));
            s--;
            for (; f < limit; f++) {
                l.append(e("<option"+ (s - f == dspYear ? ' selected ':' ') +"value=" + (s - f) + ">" + (s - f) + "</option>"));
            }
            e.bdneach(Date.monthNames, function(w, x) {
                m.append(e("<option " + (k.displayedMonth === w ? " selected" : "") + " value='" + w + "'>" + this + "</option>"));
            });
            u.append(e("<div></div>").attr(v).css(p).append(e("<div></div>").append(e('<div class="wrap-combos clearfix"></div>').append(m, l), e('<div class="dp-calendar"></div>'), e('<div class="dp-date-formated"></div>'))));
            var h = this.inline ? e(".dp-popup", this.context) : e("#dp-popup");
            if (this.showYearNavigation == false) {
                e(".dp-nav-prev-year, .dp-nav-next-year", t.context).css("display", "none");
            }
            if (this.displayClose) {
                h.append(e('<a href="#" id="dp-close">' + e.dpText.TEXT_CLOSE + "</a>").bind("click", function() {
                    t._closeCalendar();
                    return false;
                }));
            }
            t._renderCalendar();
            e(this.ele).trigger("dpDisplayed", h);
            if (!t.inline) {
                if (this.verticalPosition == e.dpConst.POS_BOTTOM) {
                    h.css("top", r.top + o.height() - h.height() + t.verticalOffset);
                }
                if (this.horizontalPosition == e.dpConst.POS_RIGHT) {
                    h.css("left", r.left + o.width() - h.width() + t.horizontalOffset);
                }
                e(document).bind("mousedown.datepicker", this._checkMouse);
            }
        },
        setRenderCallback: function(f) {
            if (f == null) {
                return;
            }
            if (f && typeof(f) == "function") {
                f = [f];
            }
            this.renderCallback = this.renderCallback.concat(f);
        },
        cellRender: function(k, f, h, g) {
            var l = this.dpController;
            var j = new Date(f.getTime());
            k.bind("click", function() {
                var o = e(this);
                if (!o.is(".disabled")) {
                    l.setSelected(j, !o.is(".selected") || !l.selectMultiple, false, true);
                    if (l.closeOnSelect) {
                        if (l.settings.autoFocusNextInput) {
                            var n = l.ele;
                            var m = false;
                            e(":input", n.form).bdneach(function() {
                                if (m) {
                                    e(this).focus();
                                    return false;
                                }
                                if (this == n) {
                                    m = true;
                                }
                            });
                        } else {
                            l.ele.focus();
                        }
                        l._closeCalendar();
                    }
                }
            });
            if (l.isSelected(j)) {
                k.addClass("selected");
                if (l.settings.selectWeek) {
                    k.parent().addClass("selectedWeek");
                }
            } else {
                if (l.selectMultiple && l.numSelected == l.numSelectable) {
                    k.addClass("unselectable");
                }
            }
        },
        _applyRenderCallbacks: function() {
            var f = this;
            e("td", this.context).bdneach(function() {
                for (var g = 0; g < f.renderCallback.length; g++) {
                    $td = e(this);
                    f.renderCallback[g].apply(this, [$td, Date.fromString($td.data("datePickerDate")), f.displayedMonth, f.displayedYear]);
                }
            });
            return;
        },
        _displayNewMonth: function(g, f, h) {
            if (!e(g).is(".disabled")) {
                this.setDisplayedMonth(this.displayedMonth + f, this.displayedYear + h, true);
            }
            g.blur();
            return false;
        },
        _rerenderCalendar: function() {
            this._clearCalendar();
            this._renderCalendar();
        },
        _renderCalendar: function() {
            e(".dp-date-formated", this.context).html((new Date()).asString(e.dpText.HEADER_FORMAT));
            e(".dp-calendar", this.context).renderCalendar(e.bExt({}, this.settings, {
                month: this.displayedMonth,
                year: this.displayedYear,
                renderCallback: this.cellRender,
                dpController: this,
                hoverClass: this.hoverClass
            }));
            if (this.displayedYear == this.startDate.getFullYear() && this.displayedMonth == this.startDate.getMonth()) {
                e(".dp-nav-prev-year", this.context).addClass("disabled");
                e(".dp-nav-prev-month", this.context).addClass("disabled");
                e(".dp-calendar td.other-month", this.context).bdneach(function() {
                    var k = e(this);
                    if (Number(k.text()) > 20) {
                        k.addClass("disabled");
                    }
                });
                var j = this.startDate.getDate();
                e(".dp-calendar td.current-month", this.context).bdneach(function() {
                    var k = e(this);
                    if (Number(k.text()) < j) {
                        k.addClass("disabled");
                    }
                });
            } else {
                e(".dp-nav-prev-year", this.context).removeClass("disabled");
                e(".dp-nav-prev-month", this.context).removeClass("disabled");
                var j = this.startDate.getDate();
                if (j > 20) {
                    var g = this.startDate.getTime();
                    var h = new Date(g);
                    h.addMonths(1);
                    if (this.displayedYear == h.getFullYear() && this.displayedMonth == h.getMonth()) {
                        e(".dp-calendar td.other-month", this.context).bdneach(function() {
                            var k = e(this);
                            if (Date.fromString(k.data("datePickerDate")).getTime() < g) {
                                k.addClass("disabled");
                            }
                        });
                    }
                }
            }
            if (this.displayedYear == this.endDate.getFullYear() && this.displayedMonth == this.endDate.getMonth()) {
                e(".dp-nav-next-year", this.context).addClass("disabled");
                e(".dp-nav-next-month", this.context).addClass("disabled");
                e(".dp-calendar td.other-month", this.context).bdneach(function() {
                    var k = e(this);
                    if (Number(k.text()) < 14) {
                        k.addClass("disabled");
                    }
                });
                var j = this.endDate.getDate();
                e(".dp-calendar td.current-month", this.context).bdneach(function() {
                    var k = e(this);
                    if (Number(k.text()) > j) {
                        k.addClass("disabled");
                    }
                });
            } else {
                e(".dp-nav-next-year", this.context).removeClass("disabled");
                e(".dp-nav-next-month", this.context).removeClass("disabled");
                var j = this.endDate.getDate();
                if (j < 13) {
                    var f = new Date(this.endDate.getTime());
                    f.addMonths(-1);
                    if (this.displayedYear == f.getFullYear() && this.displayedMonth == f.getMonth()) {
                        e(".dp-calendar td.other-month", this.context).bdneach(function() {
                            var l = e(this);
                            var k = Number(l.text());
                            if (k < 13 && k > j) {

                                l.addClass("disabled");
                            }
                        });
                    }
                }
            }
            this._applyRenderCallbacks();
        },
        _closeCalendar: function(f, g) {
            if (!g || g == this.ele) {
                e(document).unbind("mousedown.datepicker");
                e(document).unbind("keydown.datepicker");
                this._clearCalendar();
                e("#dp-popup a").unbind();
                e("#dp-popup").empty().remove();
                if (!f) {
                    e(this.ele).trigger("dpClosed", [this.getSelected()]);
                }
            }
        },
        _clearCalendar: function() {
            e(".dp-calendar td", this.context).unbind();
            e(".dp-calendar", this.context).empty();
        }
    });
    e.dpConst = {
        SHOW_HEADER_NONE: 0,
        SHOW_HEADER_SHORT: 1,
        SHOW_HEADER_LONG: 2,
        POS_TOP: 0,
        POS_BOTTOM: 1,
        POS_LEFT: 0,
        POS_RIGHT: 1,
        DP_INTERNAL_FOCUS: "dpInternalFocusTrigger"
    };
    e.dpText = {
        TEXT_PREV_YEAR: "Ano anterior",
        TEXT_PREV_MONTH: "Mês anterior",
        TEXT_NEXT_YEAR: "Próximo ano",
        TEXT_NEXT_MONTH: "Próximo mês",
        TEXT_CLOSE: "Fechar",
        TEXT_CHOOSE_DATE: "Escolha a data",
        HEADER_FORMAT: "Hoje: dd de mmmm, yyyy"
    };
    e.dpVersion = "$Id: jBDNsc.datePicker.js 108 2011-11-17 21:19:57Z kelvin.luck@gmail.com $";
    e.fn.datePicker.defaults = {
        month: undefined,
        year: undefined,
        showHeader: e.dpConst.SHOW_HEADER_SHORT,
        startDate: "01/01/1900",
        endDate: undefined,
        inline: false,
        renderCallback: null,
        createButton: true,
        showYearNavigation: true,
        closeOnSelect: true,
        displayClose: true,
        selectMultiple: false,
        numSelectable: Number.MAX_VALUE,
        clickInput: true,
        rememberViewedMonth: true,
        selectWeek: false,
        verticalPosition: e.dpConst.POS_TOP,
        horizontalPosition: e.dpConst.POS_LEFT,
        verticalOffset: 0,
        horizontalOffset: 0,
        hoverClass: "dp-hover",
        autoFocusNextInput: true
    };

    function c(f) {
        if (f._dpId) {
            return e.event._dpCache[f._dpId];
        }
        return false;
    }
    if (e.fn.bgIframe == undefined) {
        e.fn.bgIframe = function() {
            return this;
        };
    }
    e(window).bind("unload", function() {
        var g = e.event._dpCache || [];
        for (var f in g) {
            g[f] && e(g[f].ele)._dpDestroy();
        }
    });
})(jBDNsc);