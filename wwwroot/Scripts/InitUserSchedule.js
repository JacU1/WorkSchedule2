    scheduler.config.xml_date = "%Y-%m-%d %H:%i";
    scheduler.init("scheduler_here", new Date(2020, 02, 06), "week");

    // load data from backend
    scheduler.load("/api/shifts", "json");
    // connect backend to scheduler
    var dp = new dataProcessor("/api/shifts");
    dp.init(scheduler);
    // set data exchange mode
    dp.setTransactionMode("REST");
