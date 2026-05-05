import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import api from './api/axios'; 

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { AppBar, Toolbar, Typography, Button, Box, Container } from '@mui/material';
import LocalHospitalIcon from '@mui/icons-material/LocalHospital';

import LoginPage from './pages/LoginPage';
import PatientsPage from './pages/PatientsPage';
import DashboardPage from './pages/DashboardPage';
import RegisterPage from './pages/RegisterPage';
import UsersPage from './pages/UsersPage';
import DoctorsPage from './pages/DoctorsPage';
import AppointmentsPage from './pages/AppointmentsPage';
import MedicalRecordsPage from './pages/MedicalRecordsPage';

const App = () => {
    // Persistent state initialization
    const [userRole, setUserRole] = useState(() => localStorage.getItem('userRole'));

    const handleLogout = async () => {
        try {
            await api.post('/Auth/logout', {}, { withCredentials: true });
            localStorage.clear();
            setUserRole(null);
            window.location.href = '/'; 
        } catch (err) {
            localStorage.clear();
            setUserRole(null);
            window.location.href = '/';
        }
    };

    const ProtectedRoute = ({ children, adminOnly = false }) => {
        if (!userRole) return <Navigate to="/" replace />;
        if (adminOnly && userRole !== 'Admin') return <Navigate to="/dashboard" replace />;
        return children;
    };

    return (
        <Router>
            <AppBar position="static" sx={{ backgroundColor: '#2c3e50' }}>
                <Toolbar>
                    <LocalHospitalIcon sx={{ mr: 2 }} />
                    <Typography variant="h6" component="div" sx={{ flexGrow: 1, fontWeight: 'bold' }}>
                        HMS Portal
                    </Typography>
                    <Box sx={{ display: 'flex', gap: 1 }}>
                        {userRole ? (
                            <>
                                <Button color="inherit" component={Link} to="/dashboard">Dashboard</Button>
                                <Button color="inherit" component={Link} to="/patients">Patients</Button>
                                <Button color="inherit" component={Link} to="/doctors">Doctors</Button>
                                <Button color="inherit" component={Link} to="/appointments">Appointments</Button>
                                <Button color="inherit" component={Link} to="/medical-records">Medical Records</Button>
                                {userRole === 'Admin' && (
                                    <Button color="secondary" variant="contained" component={Link} to="/users" sx={{ ml: 1 }}>Manage Users</Button>
                                )}
                                <Button onClick={handleLogout} sx={{ color: '#ffc107', fontWeight: 'bold', ml: 2 }}>Logout ({userRole})</Button>
                            </>
                        ) : (
                            <>
                                <Button color="inherit" component={Link} to="/">Login</Button>
                                <Button color="inherit" component={Link} to="/register">Register</Button>
                            </>
                        )}
                    </Box>
                </Toolbar>
            </AppBar>

            <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
                <Routes>
                    {/* FIXED: Conditional redirect prevents the "split screen" */}
                    <Route 
                        path="/" 
                        element={userRole ? <Navigate to="/dashboard" replace /> : <LoginPage />} 
                    />
                    <Route path="/register" element={<RegisterPage />} />

                    <Route path="/dashboard" element={<ProtectedRoute><DashboardPage /></ProtectedRoute>} />
                    <Route path="/patients" element={<ProtectedRoute><PatientsPage /></ProtectedRoute>} />
                    <Route path="/doctors" element={<ProtectedRoute><DoctorsPage /></ProtectedRoute>} />
                    <Route path="/appointments" element={<ProtectedRoute><AppointmentsPage /></ProtectedRoute>} />
                    <Route path="/medical-records" element={<ProtectedRoute><MedicalRecordsPage /></ProtectedRoute>} />
                    <Route path="/users" element={<ProtectedRoute adminOnly={true}><UsersPage /></ProtectedRoute>} />
                    
                    <Route path="*" element={<Navigate to={userRole ? "/dashboard" : "/"} />} />
                </Routes>
            </Container>
            <ToastContainer position="bottom-right" autoClose={3000} theme="colored" />
        </Router>
    );
};

export default App;