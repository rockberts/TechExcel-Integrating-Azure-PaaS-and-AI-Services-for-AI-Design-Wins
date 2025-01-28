import requests
import streamlit as st

st.set_page_config(layout="wide")

@st.cache_data
def get_Salons():
    """Return a list of Salons from the API."""
    api_endpoint = st.secrets["api"]["endpoint"]
    response = requests.get(f"{api_endpoint}/Salons", timeout=10)
    return response

@st.cache_data
def get_Salon_bookings(Salon_id):
    """Return a list of bookings for the specified Salon."""
    api_endpoint = st.secrets["api"]["endpoint"]
    response = requests.get(f"{api_endpoint}/Salons/{Salon_id}/Bookings", timeout=10)
    return response

@st.cache_data
def invoke_chat_endpoint(question):
    """Invoke the chat endpoint with the specified question."""
    api_endpoint = st.secrets["api"]["endpoint"]
    response = requests.post(f"{api_endpoint}/Chat", data={"message": question}, timeout=10)
    return response

def main():
    """Main function for the Chat with Data Streamlit app."""

    st.write(
    """
    # API Integration via Semantic Kernel

    This Streamlit dashboard is intended to demonstrate how we can use
    the Semantic Kernel library to generate SQL statements from natural language
    queries and display them in a Streamlit app.

    ## Select a Salon
    """
    )

    # Display the list of Salons as a drop-down list
    Salons_json = get_Salons().json()
    # Reshape Salons to an object with SalonID and SalonName
    Salons = [{"id": Salon["SalonID"], "name": Salon["SalonName"]} for Salon in Salons_json]
    
    selected_Salon = st.selectbox("Salon:", Salons, format_func=lambda x: x["name"])

    # Display the list of bookings for the selected Salon as a table
    if selected_Salon:
        Salon_id = selected_Salon["id"]
        bookings = get_Salon_bookings(Salon_id).json()
        st.write("### Bookings")
        st.table(bookings)

    st.write(
        """
        ## Ask a Bookings Question

        Enter a question about Salon bookings in the text box below.
        Then select the "Submit" button to call the Chat endpoint.
        """
    )

    question = st.text_input("Question:", key="question")
    if st.button("Submit"):
        with st.spinner("Calling Chat endpoint..."):
            if question:
                response = invoke_chat_endpoint(question)
                st.write(response.text)
                st.success("Chat endpoint called successfully.")
            else:
                st.warning("Please enter a question.")

if __name__ == "__main__":
    main()
