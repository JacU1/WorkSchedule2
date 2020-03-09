
    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.init("scheduler_here", new Date(2020, 06, 06), "week");

    // load data from backend
    scheduler.load("/api/vacationsapi", "json");
    // connect backend to scheduler
    var dp = new dataProcessor("/api/vacationsapi");
    dp.init(scheduler);
    // set data exchange mode
    dp.setTransactionMode("REST");
    
scheduler.templates.event_class = function (start, end, event)
{
    if (event.type == 'notapproved') {

        return "notapproved_event"

    } else if (event.type == 'approved') {

        return "approved_event"

    } else if (event.type == 'waiting') {

        return "waiting_event"

    } else {

        return "dhx_cal_event"

           }

};



