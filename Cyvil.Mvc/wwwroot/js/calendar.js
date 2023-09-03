function getCalendarEventsArray(events) {
    var eventArr = [];
    events.forEach(evt => {
        eventArr.push ({
            title: evt.Name,
            start: evt.StartDate,
            end: evt.EndDate,
            location: evt.Location,
            details: evt.Description,
        },);
    });
    return eventArr;
}

function customCalendarOptions() {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        eventClick: function(info) {
            const modal = document.getElementById('eventDetailsModal');
            let eventModal = new bootstrap.Modal(modal);
            
            modal.addEventListener('shown.bs.modal', event => {
                document.getElementById('eventDetailsLabel').innerHTML = info.event.extendedProps.project;
                document.getElementById('evtName').innerHTML = info.event.title;
                document.getElementById('details').innerHTML = info.event.extendedProps.details;
                document.getElementById('location').innerHTML = info.event.extendedProps.location;
                document.getElementById('evt-start').innerHTML = info.event.start.toLocaleString();
                document.getElementById('evt-end').innerHTML = info.event.end.toLocaleString();
            });
            eventModal.show();
        },
        dateClick: function(info) {
            //alert(info.dateStr);
        },
        initialView: 'dayGridMonth',
        events: getCalendarEventsArray(),
    });
    calendar.render();
}