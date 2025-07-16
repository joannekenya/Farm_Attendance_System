// Ensure the DOM is fully loaded before running the script
document.addEventListener('DOMContentLoaded', () => {
    // Get references to the elements
    const sidebar = document.querySelector('.sidebar');
    const sidebarToggle = document.querySelector('.sidebar-toggle');

    // Check localStorage to see if the sidebar state was saved
    const sidebarState = localStorage.getItem('sidebarState');

    // Apply the saved sidebar state (if it exists)
    if (sidebarState === 'open') {
        sidebar.classList.add('open');
        sidebar.classList.remove('collapsed');
    } else {
        sidebar.classList.add('collapsed');
        sidebar.classList.remove('open');
    }

    // Function to toggle sidebar visibility and save the state to localStorage
    function toggleSidebar() {
        sidebar.classList.toggle('collapsed');
        sidebar.classList.toggle('open');

        // Save the state of the sidebar to localStorage
        if (sidebar.classList.contains('open')) {
            localStorage.setItem('sidebarState', 'open');
        } else {
            localStorage.setItem('sidebarState', 'collapsed');
        }
    }

    // Event listener for the sidebar toggle button
    sidebarToggle.addEventListener('click', toggleSidebar);

    // Optional: Automatically close the sidebar if the user clicks anywhere outside of it
    document.addEventListener('click', (event) => {
        if (!sidebar.contains(event.target) && !sidebarToggle.contains(event.target)) {
            sidebar.classList.remove('open');
            sidebar.classList.add('collapsed');
            localStorage.setItem('sidebarState', 'collapsed');
        }
    });
});
