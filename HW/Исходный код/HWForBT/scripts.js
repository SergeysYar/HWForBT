document.addEventListener('DOMContentLoaded', (event) => {
    loadEmployees();
    loadTimesheets();
});

function loadEmployees() {
    fetch('/api/employee')
        .then(response => response.json())
        .then(data => {
            const employeeSelect = document.getElementById('employee');
            employeeSelect.innerHTML = '';
            data.forEach(employee => {
                const option = document.createElement('option');
                option.value = employee.id;
                option.textContent = `${employee.firstName} ${employee.lastName}`;
                employeeSelect.appendChild(option);
            });
        });
}

function loadTimesheets() {
    fetch('/api/timesheet')
        .then(response => response.json())
        .then(data => {
            const timesheetList = document.getElementById('timesheet-list');
            timesheetList.innerHTML = '';
            data.forEach(timesheet => {
                const item = document.createElement('div');
                item.innerHTML = `
                    <div>
                        Сотрудник: ${timesheet.employeeName}, 
                        Причина: ${getReasonText(timesheet.reason)}, 
                        Дата начала: ${timesheet.startDate}, 
                        Продолжительность: ${timesheet.duration}, 
                        Учтено при оплате: ${timesheet.discounted ? 'Да' : 'Нет'}, 
                        Комментарий: ${timesheet.description}
                        <button onclick="editTimesheet(${timesheet.id})">Изменить</button>
                        <button onclick="deleteTimesheet(${timesheet.id})">Удалить</button>
                    </div>
                `;
                timesheetList.appendChild(item);
            });
        });
}

function getReasonText(reason) {
    switch (reason) {
        case 1: return 'Отпуск';
        case 2: return 'Больничный';
        case 3: return 'Прогул';
        default: return '';
    }
}

function submitForm() {
    const form = document.getElementById('form');
    const formData = new FormData(form);
    const timesheet = Object.fromEntries(formData.entries());
    timesheet.discounted = formData.has('discounted'); // Преобразование checkbox в boolean

    const method = timesheet.id ? 'PUT' : 'POST';
    const url = timesheet.id ? `/api/timesheet/${timesheet.id}` : '/api/timesheet';

    fetch(url, {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(timesheet)
    }).then(response => {
        if (response.ok) {
            loadTimesheets();
            form.reset();
        }
    });
}

function editTimesheet(id) {
    fetch(`/api/timesheet/${id}`)
        .then(response => response.json())
        .then(data => {
            const form = document.getElementById('form');
            form.id.value = data.id;
            form.employee.value = data.employee;
            form.reason.value = data.reason;
            form.start_date.value = data.startDate;
            form.duration.value = data.duration;
            form.discounted.checked = data.discounted;
            form.description.value = data.description;
        });
}

function deleteTimesheet(id) {
    fetch(`/api/timesheet/${id}`, {
        method: 'DELETE'
    }).then(response => {
        if (response.ok) {
            loadTimesheets();
        }
    });
}
