'use strict';

window.addEventListener('DOMContentLoaded', () => {
    const closeModals = document.querySelectorAll('[data-close]'),
        showHistoryBtn = document.querySelector('.show__history'),
        showRatesBtn = document.querySelector('.exchange__btn'),
        modals = document.querySelectorAll('.modal');

    function hideModals(modals) {
        modals.forEach(item => {
            item.style.display = 'none';
        });
        document.body.style.overflow = '';
    }

    function showModal(modal) {
        modal.style.display = 'block';
        document.body.style.overflow = 'hidden';
    }

    showHistoryBtn.addEventListener('click', e => {
        e.preventDefault();

        modals.forEach(item => {
            if (item.classList.contains('history__modal')) {
                showModal(item);
            }
        });
    });

    showRatesBtn.addEventListener('click', e => {
        e.preventDefault();

        modals.forEach(item => {
            if (item.classList.contains('exchange__modal')) {
                showModal(item);
            }
        });
    });

    closeModals.forEach(item => {
        item.addEventListener('click', e => {
            e.preventDefault();

            hideModals(modals);
        });
    });
});