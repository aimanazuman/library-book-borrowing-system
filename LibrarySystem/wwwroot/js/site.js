// API Configuration
window.API_URL = 'http://localhost:5287/api'; // Update with your API port

// Show message helper function
function showMessage(elementId, message, type) {
    const msgEl = document.getElementById(elementId);
    if (msgEl) {
        msgEl.textContent = message;
        msgEl.className = `message ${type} show`;
        setTimeout(() => {
            msgEl.classList.remove('show');
        }, 5000);
    }
}

// Load sections dynamically
async function loadSections() {
    try {
        const response = await fetch(`${window.API_URL}/sections`);
        const sections = await response.json();
        return sections;
    } catch (error) {
        console.error('Error loading sections:', error);
        return [];
    }
}

// Default rack IDs for built-in sections
const defaultRacksBySection = {
    1: ['A1', 'A2', 'A3', 'A4', 'A5', 'B1', 'B2', 'B3', 'B4', 'B5'], // Fiction
    2: ['C1', 'C2', 'C3', 'C4', 'C5', 'D1', 'D2', 'D3', 'D4', 'D5'], // Non-Fiction
    3: ['E1', 'E2', 'E3', 'E4', 'E5', 'F1', 'F2', 'F3', 'F4', 'F5']  // Reference
};

// Global racks mapping (will be populated from localStorage and Manage Sections page)
window.racksBySection = { ...defaultRacksBySection };

// Load custom racks from localStorage
function loadCustomRacks() {
    for (let i = 0; i < localStorage.length; i++) {
        const key = localStorage.key(i);
        if (key && key.startsWith('section_') && key.endsWith('_racks')) {
            const sectionId = key.match(/section_(\d+)_racks/)[1];
            const racks = JSON.parse(localStorage.getItem(key));
            window.racksBySection[sectionId] = racks;
        }
    }
}

// Initialize custom racks on page load
loadCustomRacks();

// Update rack dropdown based on section
function updateRackDropdown(sectionId) {
    const rackSelect = document.getElementById('bookRackId');
    if (!rackSelect) return;

    rackSelect.innerHTML = '<option value="">Select Rack ID</option>';

    if (sectionId && window.racksBySection[sectionId]) {
        window.racksBySection[sectionId].forEach(rack => {
            const option = document.createElement('option');
            option.value = rack;
            option.textContent = rack;
            rackSelect.appendChild(option);
        });
    } else {
        // If no racks configured, allow manual entry
        rackSelect.innerHTML = '<option value="">No racks configured for this section</option>';
    }
}
