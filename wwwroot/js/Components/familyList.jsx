
 class FamilyList extends React.Component {
    state = {
        plant: [],
        allPlants: []
    };

    componentDidMount() {
        axios
            .get('https://localhost:44311/api/Family')
            .then(({ data }) => {
                this.setState({
                    plant: data,
                    allPlants: data // array data from JSON stored in these
                });
            })
            .catch((err) => { });
    }

    _onKeyUp = (e) => {
        // filter plant list by title using onKeyUp function
        const plant = this.state.allPlants.filter((item) =>
            item.familyName
                .toLowerCase()
                .includes(e.target.value.toLowerCase()) ||
            item.familyLatin
                .toLowerCase()
                .includes(e.target.value.toLowerCase())

        );
        this.setState({ plant });
    };

    render() {
        return (
            <div className="plantcontainer">
                <div className="search-outer">
                    <form
                        role="search"
                        method="get"
                        id="searchform"
                        className="searchform"
                        action=""
                        autoComplete="off"
                    >
                        {/* input field activates onKeyUp function on state change */}
                        <input
                            type="search"
                            onChange={this._onKeyUp}
                            name="s"
                            id="s"
                            placeholder="Sök efter en växt"
                        />
                    </form>
                </div>

                <ul className="plantList">
                    {/* plant items mapped in a list linked to onKeyUp function */}
                    {this.state.plant.map((item, index) => (
                        <li className={"block-" + index} id="displayPlant" key={item.familyId}>
                            <a className="title" href={'/Families/Details/' + item.familyId}>
                                <h3>{item.familyName}</h3>
                            </a>
                            <h4 className="undertitle">{item.familyLatin}</h4>
                        </li>
                    ))}
                </ul>
            </div>
        );
    }
}

ReactDOM.render(<FamilyList />, document.getElementById('familyList'));
