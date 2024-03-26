import PropTypes from "prop-types";

export default function PureSvgNodeElement({ nodeDatum, toggleNode, onNodeClick }) {
    const foreignObjectSize = { width: 200, height: 200 }; // Example size, adjust based on your content
    const translateX = 30;
    const translateY = -20;

    return (
        <>
            <circle r={20} onClick={toggleNode}></circle>
            <foreignObject
                x="0"
                width={foreignObjectSize.width}
                height={foreignObjectSize.height}
                transform={`translate(${translateX}, ${translateY})`} // Apply the translate transform
            >
                <div style={{ height: "100%", width: "100%" }}>
                    <p onClick={onNodeClick} style={{ margin: 0, cursor: "pointer" }}>
                        <b>{nodeDatum.name}</b>
                    </p>
                    {/* Displaying the number of credits if available */}
                    {nodeDatum.attributes && nodeDatum.attributes.size && (
                        <p style={{ margin: "1px 0" }}>Size: {nodeDatum.attributes.size}</p>
                    )}
                    {/* Adding a View button if courseGroupingId is available */}
                    {nodeDatum.attributes && nodeDatum.attributes.id && (
                        <p
                            style={{ margin: 0, cursor: "pointer", color: "#912338" }}
                            onClick={() => {
                                if (nodeDatum.attributes.email) {
                                    window.open(`/profile?email=${nodeDatum.attributes.email}`, "_blank");
                                } else {
                                    window.open(`/GroupDetails?groupId=${nodeDatum.attributes.id}`, "_blank");
                                }
                            }}
                        >
                            View
                        </p>
                    )}
                </div>
            </foreignObject>
        </>
    );
}

PureSvgNodeElement.propTypes = {
    nodeDatum: PropTypes.shape({
        name: PropTypes.string.isRequired,
        attributes: PropTypes.shape({
            size: PropTypes.number,
            id: PropTypes.string,
            email: PropTypes.string,
        }),
    }).isRequired,
    toggleNode: PropTypes.func.isRequired,
    onNodeClick: PropTypes.func.isRequired,
};
