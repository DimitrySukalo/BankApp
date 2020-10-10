'use strict';

window.addEventListener('DOMContentLoaded', () => {
    const closeModal = document.querySelector('[data-close]'),
        showHistoryBtn = document.querySelector('.show__history'),
        modalHistory = document.querySelector('.modal');

    function hideModals(modal) {
        modal.style.display = 'none';
        document.body.style.overflow = '';
    }

    function showModal(modal) {
        modal.style.display = 'block';
        document.body.style.overflow = 'hidden';
    }

    showHistoryBtn.addEventListener('click', e => {
        e.preventDefault();

        showModal(modalHistory);
    });

    closeModal.addEventListener('click', e => {
        e.preventDefault();

        hideModals(modalHistory);
    });

});