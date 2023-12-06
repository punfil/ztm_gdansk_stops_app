//npm run cypress:open
describe('Login', () => {
    it('successfully logs in', () => {
        // Mock server response for successful login
        cy.intercept('POST', '**/login/*', {
            statusCode: 200,
            body: {
                // Provide any mock data needed for a successful login
                userId: 123,
                username: 'testuser',
            },
        }).as('loginRequest');

        // Visit the login page or trigger the login action
        // You should replace the URL with the actual URL of your login page
        cy.visit('https://localhost:5173');
        cy.get('[data-cy=loginn]').click();
        // Fill in the login form
        cy.get('[data-cy=username]').type('wpanfil');
        cy.get('[data-cy=password]').type('1234');

        // Submit the form
        cy.get('[data-cy=submit]').click();

        // Wait for the login request to complete
        cy.wait('@loginRequest');

        // Check if the user is logged in
        cy.get('[data-cy=logoutt]').should('exist');
    });

    it('handles failed login', () => {
        // Mock server response for failed login
        cy.intercept('POST', '**/login/*', {
            statusCode: 401,
            body: {
                // Provide any mock data needed for a failed login
                error: 'Invalid credentials',
            },
        }).as('loginRequest');

        // Visit the login page or trigger the login action
        // You should replace the URL with the actual URL of your login page
        cy.visit('https://localhost:5173');
        cy.get('[data-cy=loginn]').click();

        // Fill in the login form
        cy.get('[data-cy=username]').type('invaliduser');
        cy.get('[data-cy=password]').type('invalidpassword');

        // Submit the form
        cy.get('[data-cy=submit]').click();

        // Wait for the login request to complete
        cy.wait('@loginRequest');

        // Check if an error message is displayed
        cy.get('@loginRequest').then(() => {
            cy.get('[data-cy=msg]').should('have.text', 'Failed to login invaliduser');
        });
    });
});