import React, { useState, useEffect } from 'react';
import api from '../api/axios';
import { toast } from 'react-toastify';

// MUI Imports
import { 
    Container, Typography, Paper, Box, TextField, Button, 
    Table, TableBody, TableCell, TableContainer, TableHead, TableRow, 
    IconButton, CircularProgress, Dialog, DialogTitle, DialogContent, DialogActions 
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import PersonAddIcon from '@mui/icons-material/PersonAdd';

const PatientsPage = () => {
    const [patients, setPatients] = useState([]);
    const [loading, setLoading] = useState(true);
    
    // State for Edit Modal
    const [openEdit, setOpenEdit] = useState(false);
    const [selectedPatient, setSelectedPatient] = useState({ id: '', name: '', email: '' });

    // State for Add Form
    const [newName, setNewName] = useState('');
    const [newEmail, setNewEmail] = useState('');

    useEffect(() => { 
        fetchPatients(); 
    }, []);

    const fetchPatients = async () => {
        try {
            const res = await api.get('/Patients');
            setPatients(res.data);
            setLoading(false);
        } catch (err) {
            toast.error("Failed to load patients from the server.");
            setLoading(false);
        }
    };

    const handleEditClick = (p) => {
        // Correctly maps the PascalCase from your C# model to the frontend state
        setSelectedPatient({
            id: p.id || p.Id,
            name: p.name || p.Name,
            email: p.email || p.Email
        });
        setOpenEdit(true);
    };

    const handleSaveEdit = async () => {
        try {
            // Sends only Name and Email to match your C# Patient model
            await api.put(`/Patients/${selectedPatient.id}`, {
                Id: selectedPatient.id,
                Name: selectedPatient.name,
                Email: selectedPatient.email
            });
            toast.success("Patient updated successfully!");
            setOpenEdit(false);
            fetchPatients(); 
        } catch (err) {
            toast.error("Failed to update. Check if the Email is valid.");
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm("Are you sure you want to delete this patient?")) return;
        try {
            await api.delete(`/Patients/${id}`);
            toast.success("Patient deleted.");
            fetchPatients();
        } catch (err) {
            toast.error("Delete failed.");
        }
    };

    const handleAdd = async (e) => {
        e.preventDefault();
        try {
            // Matches the [FromBody] expectation of your backend
            await api.post('/Patients', { 
                Name: newName, 
                Email: newEmail 
            });
            toast.success("Patient added successfully!");
            setNewName(''); 
            setNewEmail('');
            fetchPatients();
        } catch (err) {
            toast.error("Error adding patient. Ensure the backend is running.");
        }
    };

    if (loading) return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 5 }}><CircularProgress /></Box>;

    return (
        <Container maxWidth="lg">
            <Typography variant="h4" sx={{ fontWeight: 'bold', mb: 3 }}>Patient Management</Typography>

            {/* ADD FORM */}
            <Paper sx={{ p: 3, mb: 4 }}>
                <Box component="form" onSubmit={handleAdd} sx={{ display: 'flex', gap: 2, flexWrap: 'wrap' }}>
                    <TextField 
                        label="Patient Name" 
                        value={newName} 
                        onChange={(e) => setNewName(e.target.value)} 
                        required 
                        sx={{ flexGrow: 1 }}
                    />
                    <TextField 
                        label="Email Address" 
                        type="email"
                        value={newEmail} 
                        onChange={(e) => setNewEmail(e.target.value)} 
                        required 
                        sx={{ flexGrow: 1 }}
                    />
                    <Button type="submit" variant="contained" startIcon={<PersonAddIcon />}>Add Patient</Button>
                </Box>
            </Paper>

            {/* PATIENTS TABLE */}
            <TableContainer component={Paper} sx={{ borderRadius: 2, boxShadow: 3 }}>
                <Table>
                    <TableHead sx={{ bgcolor: '#f5f5f5' }}>
                        <TableRow>
                            <TableCell sx={{ fontWeight: 'bold' }}>ID</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Name</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Email</TableCell>
                            <TableCell align="right" sx={{ fontWeight: 'bold' }}>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {patients.map((p) => (
                            <TableRow key={p.id || p.Id}>
                                <TableCell>{p.id || p.Id}</TableCell>
                                <TableCell sx={{ fontWeight: 'bold' }}>{p.name || p.Name}</TableCell>
                                <TableCell>{p.email || p.Email}</TableCell>
                                <TableCell align="right">
                                    <IconButton color="primary" onClick={() => handleEditClick(p)}>
                                        <EditIcon />
                                    </IconButton>
                                    <IconButton color="error" onClick={() => handleDelete(p.id || p.Id)}>
                                        <DeleteIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>

            {/* EDIT MODAL */}
            <Dialog open={openEdit} onClose={() => setOpenEdit(false)} fullWidth maxWidth="xs">
                <DialogTitle sx={{ fontWeight: 'bold' }}>Edit Patient Info</DialogTitle>
                <DialogContent sx={{ pt: 2, display: 'flex', flexDirection: 'column', gap: 2 }}>
                    <TextField 
                        label="Full Name" fullWidth variant="outlined"
                        value={selectedPatient.name} 
                        onChange={(e) => setSelectedPatient({...selectedPatient, name: e.target.value})} 
                    />
                    <TextField 
                        label="Email Address" fullWidth variant="outlined"
                        value={selectedPatient.email} 
                        onChange={(e) => setSelectedPatient({...selectedPatient, email: e.target.value})} 
                    />
                </DialogContent>
                <DialogActions sx={{ p: 3 }}>
                    <Button onClick={() => setOpenEdit(false)} color="inherit">Cancel</Button>
                    <Button variant="contained" onClick={handleSaveEdit}>Update Patient</Button>
                </DialogActions>
            </Dialog>
        </Container>
    );
};

export default PatientsPage;