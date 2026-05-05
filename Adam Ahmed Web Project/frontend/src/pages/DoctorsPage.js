import React, { useState, useEffect } from 'react';
import api from '../api/axios';

// --- MATERIAL-UI IMPORTS ---
import { 
    Container, Typography, Paper, Box, TextField, Button, 
    Table, TableBody, TableCell, TableContainer, TableHead, TableRow, 
    CircularProgress, IconButton, Chip
} from '@mui/material';
import DeleteIcon from '@mui/icons-material/Delete';
import MedicalServicesIcon from '@mui/icons-material/MedicalServices';
import PersonAddAlt1Icon from '@mui/icons-material/PersonAddAlt1';
import WorkspacePremiumIcon from '@mui/icons-material/WorkspacePremium';

const DoctorsPage = () => {
    const [doctors, setDoctors] = useState([]);
    const [loading, setLoading] = useState(true);

    // Form State
    const [name, setName] = useState('');
    const [specialization, setSpecialization] = useState('');
    const [experience, setExperience] = useState('');

    useEffect(() => {
        fetchDoctors();
    }, []);

    const fetchDoctors = async () => {
        try {
            const response = await api.get('/Doctors');
            setDoctors(response.data);
            setLoading(false);
        } catch (err) {
            console.error("Failed to load doctors", err);
            setLoading(false);
        }
    };

    const handleAddDoctor = async (e) => {
        e.preventDefault();
        try {
            await api.post('/Doctors', {
                name: name,
                specialization: specialization,
                yearsOfExperience: parseInt(experience)
            });

            setName('');
            setSpecialization('');
            setExperience('');
            fetchDoctors();
        } catch (err) {
            alert("Error adding doctor. Please check your inputs.");
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm("Are you sure you want to remove this doctor?")) return;
        try {
            await api.delete(`/Doctors/${id}`);
            fetchDoctors();
        } catch (err) {
            alert("Failed to delete doctor.");
        }
    };

    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="50vh">
                <CircularProgress />
            </Box>
        );
    }

    return (
        <Container maxWidth="lg" sx={{ mt: 5, mb: 5 }}>
            {/* --- PAGE HEADER --- */}
            <Box display="flex" alignItems="center" gap={2} mb={4}>
                <MedicalServicesIcon color="primary" sx={{ fontSize: 40 }} />
                <Typography variant="h4" fontWeight="bold" color="text.primary">
                    Medical Staff Directory
                </Typography>
            </Box>

            {/* --- ADD NEW DOCTOR FORM --- */}
            <Paper elevation={3} sx={{ p: 4, mb: 5, borderRadius: 2 }}>
                <Box display="flex" alignItems="center" gap={1} mb={3}>
                    <PersonAddAlt1Icon color="primary" />
                    <Typography variant="h6" fontWeight="600" color="primary">
                        Add New Physician
                    </Typography>
                </Box>
                
                <Box component="form" onSubmit={handleAddDoctor} sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
                    <Box sx={{ display: 'flex', gap: 3, flexDirection: { xs: 'column', md: 'row' } }}>
                        <TextField 
                            label="Doctor Name" 
                            variant="outlined" 
                            required 
                            fullWidth 
                            value={name} 
                            onChange={(e) => setName(e.target.value)} 
                        />
                        <TextField 
                            label="Specialization (e.g. Cardiology)" 
                            variant="outlined" 
                            required 
                            fullWidth 
                            value={specialization} 
                            onChange={(e) => setSpecialization(e.target.value)} 
                        />
                        <TextField 
                            label="Years of Experience" 
                            type="number"
                            variant="outlined" 
                            required 
                            sx={{ minWidth: '150px' }}
                            value={experience} 
                            onChange={(e) => setExperience(e.target.value)} 
                        />
                    </Box>

                    <Button 
                        type="submit" 
                        variant="contained" 
                        color="primary" 
                        size="large" 
                        sx={{ alignSelf: 'flex-start', px: 4, py: 1.5, fontWeight: 'bold' }}
                    >
                        Add to Staff
                    </Button>
                </Box>
            </Paper>

            {/* --- DOCTORS TABLE --- */}
            <Typography variant="h5" fontWeight="600" mb={2}>
                Our Physicians
            </Typography>
            <TableContainer component={Paper} elevation={3} sx={{ borderRadius: 2, overflow: 'hidden' }}>
                <Table sx={{ minWidth: 650 }}>
                    <TableHead sx={{ backgroundColor: '#f4f6f8' }}>
                        <TableRow>
                            <TableCell sx={{ fontWeight: 'bold' }}>Name</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Specialization</TableCell>
                            <TableCell sx={{ fontWeight: 'bold' }}>Experience</TableCell>
                            <TableCell align="right" sx={{ fontWeight: 'bold' }}>Actions</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {doctors.map((doc) => (
                            <TableRow key={doc.id} hover>
                                <TableCell sx={{ fontWeight: 'bold' }}>Dr. {doc.name}</TableCell>
                                <TableCell>
                                    <Chip 
                                        label={doc.specialization} 
                                        color="primary" 
                                        variant="outlined" 
                                        size="small" 
                                        sx={{ fontWeight: '500' }}
                                    />
                                </TableCell>
                                <TableCell>
                                    <Box display="flex" alignItems="center" gap={0.5}>
                                        <WorkspacePremiumIcon sx={{ fontSize: 18, color: 'gold' }} />
                                        {doc.yearsOfExperience} Years
                                    </Box>
                                </TableCell>
                                <TableCell align="right">
                                    <IconButton color="error" onClick={() => handleDelete(doc.id)} size="small">
                                        <DeleteIcon />
                                    </IconButton>
                                </TableCell>
                            </TableRow>
                        ))}
                        {doctors.length === 0 && (
                            <TableRow>
                                <TableCell colSpan={4} align="center" sx={{ py: 5, color: 'text.secondary' }}>
                                    No doctors found in the staff directory.
                                </TableCell>
                            </TableRow>
                        )}
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    );
};

export default DoctorsPage;