function List() {
    const [plants, setPlants] = React.useState(null);

    const fetchData = async () => {
        const response = await axios.get(
            'https://localhost:44311/api/Plant'
        );

        setPlants(response.data);
    };



    return (
        <div className="List">
            <h2>Fetch a list from an API and display it</h2>

            {/* Fetch data from API */}
            <div>
                <button className="fetch-button" onClick={fetchData}>
                    Hämta växter
        </button>
                <br />
            </div>

            {/* Display data from API */}
            <div className="plants">
                {plants &&
                    plants.map((plant, index) => {
                        return (
                            <div className="plant" key={index}>
                                <h2>{plant.plantName}</h2>

                                <div className="details">

                                    <p>📖: {plant.plantName}</p>
                                    <p>🏘️: {plant.latin}</p>
                                </div>
                            </div>
                        );
                    })}
            </div>
        </div>
    );
}

export default List;