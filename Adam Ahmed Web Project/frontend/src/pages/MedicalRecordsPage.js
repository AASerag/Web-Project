import React, { useState, useEffect } from 'react';
import api from '../api/axios';
import { toast } from 'react-toastify';
import { 
    Container, Typography, Paper, Box, TextField, Button, 
    Table, TableBody, TableCell, TableContainer, TableHead, TableRow, 
    IconButton, CircularProgress, MenuItem, Select, FormControl, InputLabel,
    Dialog, DialogTitle, DialogContent, DialogActions
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import NoteAddIcon from '@mui/icons-material/NoteAdd';

const MedicalRecordsPage = () => {
    const [records, setRecords] = useState([]);
    const [patients, setPatients] = useState([]);
    const [doctors, setDoctors] = useState([]);
    const [loading, setLoading] = useState(true);

    // Form State
    const [diagnosis, setDiagnosis] = useState('');
    const [prescription, setPrescription] = useState('');
    const [notes, setNotes] = useState('');
    const [selectedPatientId, setSelectedPatientId] = useState('');
    const [selectedDoctorId, setSelectedDoctorId] = useState('');

    useEffect(() => { fetchData(); }, []);

    const fetchData = async () => {
        try {
            const [recordRes, patientRes, doctorRes] = await Promise.all([
                api.get('/MedicalRecords'),
                api.get('/Patients'),
                api.get('/Doctors')
            ]);
            setRecords(recordRes.data);
            setPatients(patientRes.data);
            setDoctors(doctorRes.data);
            setLoading(false);
        } catch (err) {
            toast.error("Failed to sync with backend.");
            setLoading(false);
        }
    };

    const handleAddRecord = async (e) => {
        e.preventDefault();
        try {
            // MATCHES MedicalRecordCreateDto
            await api.post('/MedicalRecords', {
                Diagnosis: diagnosis,
                Prescription: prescription,
                Notes: notes,
                PatientId: Number(selectedPatientId),
                DoctorId: Number(selectedDoctorId)
            });
            toast.success("Record created successfully!");
            resetForm();
            fetchData();
        } catch (err) {
            toast.error("Error: Ensure this patient doesn't already have a record.");
        }
    };

    const resetForm = () => {
        setDiagnosis(''); setPrescription(''); setNotes('');
        setSelectedPatientId(''); setSelectedDoctorId('');
    };

    return (
        <Container maxWidth="lg">
            <Typography variant="h4" sx={{ fontWeight: 'bold', mb: 3 }}>Medical Records</Typography>

            {/* ADD FORM */}
            <Paper sx={{ p: 3, mb: 4 }}>
                <Box component="form" onSubmit={handleAddRecord} sx={{ display: 'flex', gap: 2, flexWrap: 'wrap' }}>
                    <FormControl sx={{ minWidth: 150 }}>
                        <InputLabel>Patient</InputLabel>
                        <Select value={selectedPatientId} label="Patient" onChange={(e) => setSelectedPatientId(e.target.value)} required>
                            {patients.map((p) => <MenuItem key={p.id || p.Id} value={p.id || p.Id}>{p.name || p.Name}</MenuItem>)}
                        </Select>
                    </FormControl>

                    <FormControl sx={{ minWidth: 150 }}>
                        <InputLabel>Doctor</InputLabel>
                        <Select value={selectedDoctorId} label="Doctor" onChange={(e) => setSelectedDoctorId(e.target.value)} required>
                            {doctors.map((d) => <MenuItem key={d.id || d.Id} value={d.id || d.Id}>{d.name || d.Name}</MenuItem>)}
                        </Select>
                    </FormControl>

                    <TextField label="Diagnosis" value={diagnosis} onChange={(e) => setDiagnosis(e.target.value)} required />
                    <TextField label="Prescription" value={prescription} onChange={(e) => setPrescription(e.target.value)} required />
                    <TextField label="Notes" value={notes} onChange={(e) => setNotes(e.target.value)} />
                    
                    <Button type="submit" variant="contained" startIcon={<NoteAddIcon />}>Add Record</Button>
                </Box>
            </Paper>

            {/* DATA TABLE */}
            <TableContainer component={Paper}>
                <Table>
                    <TableHead sx={{ bgcolor: '#f5f5f5' }}>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Patient</TableCell>
                            <TableCell>Doctor</TableCell>
                            <TableCell>Diagnosis</TableCell>
                            <TableCell>Prescription</TableCell>
                            <TableCell align="right">Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {records.map((r) => (
                            <TableRow key={r.id || r.Id}>
                                <TableCell>{r.id || r.Id}</TableCell>
                                {/* Correctly show names from DTO */}
                                <TableCell sx={{ fontWeight: 'bold' }}>{r.patientName || "N/A"}</TableCell>
                                <TableCell>{r.doctorName || "N/A"}</TableCell>
                                <TableCell>{r.diagnosis || r.Diagnosis}</TableCell>
                                <TableCell>{r.prescription || r.Prescription}</TableCell>
                                <TableCell align="right">
                                    <IconButton color="error" onClick={() => api.delete(`/MedicalRecords/${r.id || r.Id}`).then(() => fetchData())}>
                                        <DeleteIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    );
};

export default MedicalRecordsPage;