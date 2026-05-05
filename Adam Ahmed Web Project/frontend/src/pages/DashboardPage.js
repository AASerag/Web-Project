import React, { useState, useEffect } from 'react';
import api from '../api/axios';
import { 
    Container, Typography, Grid, Paper, Box, 
    CircularProgress, Card, CardContent, Divider 
} from '@mui/material';
import PeopleIcon from '@mui/icons-material/People';
import MedicalServicesIcon from '@mui/icons-material/MedicalServices';
import EventAvailableIcon from '@mui/icons-material/EventAvailable';
import AssignmentIcon from '@mui/icons-material/Assignment';

const Dashboard = () => {
    const [stats, setStats] = useState({ patients: 0, doctors: 0, appointments: 0, records: 0 });
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchStats = async () => {
            try {
                // Fetching all data to count lengths
                const [p, d, a, r] = await Promise.all([
                    api.get('/Patients'),
                    api.get('/Doctors'),
                    api.get('/Appointments'),
                    api.get('/MedicalRecords')
                ]);
                setStats({
                    patients: p.data.length,
                    doctors: d.data.length,
                    appointments: a.data.length,
                    records: r.data.length
                });
                setLoading(false);
            } catch (err) {
                console.error("Dashboard load failed", err);
                setLoading(false);
            }
        };
        fetchStats();
    }, []);

    const StatCard = ({ title, count, icon, color }) => (
        <Card sx={{ height: '100%', display: 'flex', alignItems: 'center', boxShadow: 3, borderRadius: 3 }}>
            <Box sx={{ display: 'flex', alignItems: 'center', p: 2, width: '100%' }}>
                <Box sx={{ 
                    backgroundColor: `${color}.light`, 
                    borderRadius: 2, 
                    p: 1.5, 
                    display: 'flex', 
                    mr: 2 
                }}>
                    {React.cloneElement(icon, { sx: { color: `${color}.main`, fontSize: 35 } })}
                </Box>
                <CardContent sx={{ p: '0 !important' }}>
                    <Typography variant="subtitle2" color="text.secondary" fontWeight="600">
                        {title}
                    </Typography>
                    <Typography variant="h4" fontWeight="bold">
                        {count}
                    </Typography>
                </CardContent>
            </Box>
        </Card>
    );

    if (loading) return (
        <Box display="flex" justifyContent="center" alignItems="center" minHeight="80vh">
            <CircularProgress />
        </Box>
    );

    return (
        <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
            <Box mb={4}>
                <Typography variant="h4" fontWeight="bold" gutterBottom>
                    Hospital Overview
                </Typography>
                <Typography color="text.secondary">
                    Welcome back, Administrator. Here is what's happening today.
                </Typography>
            </Box>

            <Grid container spacing={3} mb={5}>
                <Grid item xs={12} sm={6} md={3}>
                    <StatCard title="Total Patients" count={stats.patients} icon={<PeopleIcon />} color="primary" />
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
                    <StatCard title="Total Doctors" count={stats.doctors} icon={<MedicalServicesIcon />} color="success" />
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
                    <StatCard title="Appointments" count={stats.appointments} icon={<EventAvailableIcon />} color="warning" />
                </Grid>
                <Grid item xs={12} sm={6} md={3}>
                    <StatCard title="Medical Records" count={stats.records} icon={<AssignmentIcon />} color="info" />
                </Grid>
            </Grid>

            <Paper sx={{ p: 3, borderRadius: 3, boxShadow: 2 }}>
                <Typography variant="h6" fontWeight="bold" mb={2}>Project Information</Typography>
                <Divider sx={{ mb: 2 }} />
                <Typography variant="body1" paragraph>
                    <strong>Project:</strong> Web Engineering Medical Management System
                </Typography>
                <Typography variant="body1" paragraph>
                    <strong>Prepared by:</strong> Adam Ahmed Serag
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    This system manages patient registration, physician scheduling, and clinical record keeping using a .NET 7 Web API and React 18.
                </Typography>
            </Paper>
        </Container>
    );
};

export default Dashboard;