import React, { useState, useEffect } from 'react';
import api from '../api/axios';

const AppointmentsPage = () => {
    const [appointments, setAppointments] = useState([]);
    const [patients, setPatients] = useState([]);
    const [doctors, setDoctors] = useState([]);
    
    // Form State
    const [selectedPatient, setSelectedPatient] = useState('');
    const [selectedDoctor, setSelectedDoctor] = useState('');
    const [date, setDate] = useState('');
    const [reason, setReason] = useState('');

    useEffect(() => {
        fetchInitialData();
    }, []);

    const fetchInitialData = async () => {
        try {
            const [appts, pats, docs] = await Promise.all([
                api.get('/Appointments'),
                api.get('/Patients'),
                api.get('/Doctors')
            ]);
            setAppointments(appts.data);
            setPatients(pats.data);
            setDoctors(docs.data);
        } catch (err) {
            console.error("Failed to fetch data", err);
        }
    };

    const handleBook = async (e) => {
        e.preventDefault();
        try {
            await api.post('/Appointments', {
                appointmentDate: date,
                patientId: parseInt(selectedPatient),
                doctorId: parseInt(selectedDoctor),
                reason: reason
            });
            // Reset form and refresh list
            setReason('');
            fetchInitialData();
            alert("Appointment Booked Successfully!");
        } catch (err) {
            alert("Error booking: " + (err.response?.data || "Check fields"));
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm("Cancel this appointment?")) return;
        await api.delete(`/Appointments/${id}`);
        fetchInitialData();
    };

    return (
        <div style={{ padding: '20px', maxWidth: '1000px', margin: '0 auto' }}>
            <h2>Appointment Management</h2>
            
            {/* BOOKING FORM */}
            <div style={{ backgroundColor: '#f8f9fa', padding: '20px', borderRadius: '8px', marginBottom: '30px' }}>
                <h4>Book New Appointment</h4>
                <form onSubmit={handleBook} style={{ display: 'flex', flexWrap: 'wrap', gap: '15px' }}>
                    <select value={selectedPatient} onChange={(e) => setSelectedPatient(e.target.value)} required style={{ padding: '8px', flex: 1 }}>
                        <option value="">-- Select Patient --</option>
                        {patients.map(p => <option key={p.id} value={p.id}>{p.name}</option>)}
                    </select>

                    <select value={selectedDoctor} onChange={(e) => setSelectedDoctor(e.target.value)} required style={{ padding: '8px', flex: 1 }}>
                        <option value="">-- Select Doctor --</option>
                        {doctors.map(d => <option key={d.id} value={d.id}>{d.name}</option>)}
                    </select>

                    <input type="datetime-local" value={date} onChange={(e) => setDate(e.target.value)} required style={{ padding: '8px' }} />
                    <input type="text" placeholder="Reason for visit" value={reason} onChange={(e) => setReason(e.target.value)} style={{ padding: '8px', flex: 2 }} />
                    
                    <button type="submit" style={{ padding: '8px 20px', backgroundColor: '#28a745', color: 'white', border: 'none', borderRadius: '4px', cursor: 'pointer' }}>Book Appointment</button>
                </form>
            </div>

            {/* APPOINTMENT LIST */}
            <table style={{ width: '100%', borderCollapse: 'collapse' }}>
                <thead>
                    <tr style={{ borderBottom: '2px solid #333' }}>
                        <th style={{ textAlign: 'left', padding: '10px' }}>Date & Time</th>
                        <th style={{ textAlign: 'left', padding: '10px' }}>Patient ID</th>
                        <th style={{ textAlign: 'left', padding: '10px' }}>Doctor ID</th>
                        <th style={{ textAlign: 'left', padding: '10px' }}>Reason</th>
                        <th style={{ textAlign: 'left', padding: '10px' }}>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {appointments.map(appt => (
                        <tr key={appt.id} style={{ borderBottom: '1px solid #ddd' }}>
                            <td style={{ padding: '10px' }}>{new Date(appt.appointmentDate).toLocaleString()}</td>
                            <td style={{ padding: '10px' }}>{appt.patientId}</td>
                            <td style={{ padding: '10px' }}>{appt.doctorId}</td>
                            <td style={{ padding: '10px' }}>{appt.reason}</td>
                            <td style={{ padding: '10px' }}>
                                <button onClick={() => handleDelete(appt.id)} style={{ backgroundColor: '#dc3545', color: 'white', border: 'none', padding: '5px 10px', borderRadius: '4px' }}>Cancel</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default AppointmentsPage;